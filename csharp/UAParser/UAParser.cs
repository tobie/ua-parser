#region Apache License, Version 2.0
// 
// Copyright 2014 Atif Aziz
// Portions Copyright 2012 Søren Enemærke
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace UAParser
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using YamlDotNet.RepresentationModel;

    #endregion

    public sealed class Device
    {
        public Device(string family, bool isSpider)
        {
            Family = family;
            IsSpider = isSpider;
        }

        public string Family { get; private set; }
        public bool IsSpider { get; private set; }

        public override string ToString() { return Family; }
    }

    // ReSharper disable once InconsistentNaming
    public sealed class OS
    {
        public OS(string family, string major, string minor, string patch, string patchMinor)
        {
            Family     = family;
            Major      = major;
            Minor      = minor;
            Patch      = patch;
            PatchMinor = patchMinor;
        }

        public string Family     { get; private set; }
        public string Major      { get; private set; }
        public string Minor      { get; private set; }
        public string Patch      { get; private set; }
        public string PatchMinor { get; private set; }

        public override string ToString()
        {
            var version = VersionString.Format(Major, Minor, Patch, PatchMinor);
            return Family + (!string.IsNullOrEmpty(version) ? " " + version : null);
        }
    }

    public sealed class UserAgent
    {
        public UserAgent(string family, string major, string minor, string patch)
        {
            Family = family;
            Major  = major;
            Minor  = minor;
            Patch  = patch;
        }

        public string Family { get; private set; }
        public string Major  { get; private set; }
        public string Minor  { get; private set; }
        public string Patch  { get; private set; }

        public override string ToString()
        {
            var version = VersionString.Format(Major, Minor, Patch);
            return Family + (!string.IsNullOrEmpty(version) ? " " + version : null);
        }
    }

    static class VersionString
    {
        public static string Format(params string[] parts)
        {
            return string.Join(".", parts.Where(v => !String.IsNullOrEmpty(v)).ToArray());
        }
    }

    public class ClientInfo
    {
        // ReSharper disable once InconsistentNaming
        public OS OS { get; private set; }
        public Device Device { get; private set; }
        public UserAgent UserAgent { get; private set; }

        public ClientInfo(OS os, Device device, UserAgent userAgent)
        {
            OS = os;
            Device = device;
            UserAgent = userAgent;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", OS, Device, UserAgent);
        }
    }

    public sealed class Parser
    {
        readonly Func<string, OS> _osParser;
        readonly Func<string, Device> _deviceParser;
        readonly Func<string, UserAgent> _userAgentParser;

        Parser(string yaml)
        {
            var ys = new YamlStream();
            using (var reader = new StringReader(yaml))
                ys.Load(reader);

            var entries =
                from doc in ys.Documents
                select doc.RootNode as YamlMappingNode into rn
                where rn != null
                from e in rn.Children
                select new { Key = e.Key as YamlScalarNode, 
                             Value = e.Value as YamlSequenceNode } into e
                where e.Key != null && e.Value != null
                select e;

            var config = entries.ToDictionary(e => e.Key.Value, 
                                              e => e.Value, 
                                              StringComparer.OrdinalIgnoreCase);

            const string other = "Other";
            var defaultDevice = new Device(other, isSpider: false);

            _userAgentParser = CreateParser(Read(config.Find("user_agent_parsers"), Config.UserAgent), new UserAgent(other, null, null, null));
            _osParser        = CreateParser(Read(config.Find("os_parsers"        ), Config.OS),        new OS(other, null, null, null, null));
            _deviceParser    = CreateParser(Read(config.Find("device_parsers"    ), Config.Device),    defaultDevice.Family, f => defaultDevice.Family == f ? defaultDevice : new Device(f, "Spider".Equals(f, StringComparison.InvariantCultureIgnoreCase)));
        }

        public static Parser FromYaml(string yaml)     { return new Parser(yaml); }
        public static Parser FromYamlFile(string path) { return new Parser(File.ReadAllText(path)); }

        public static Parser GetDefault()
        {
            using (var stream = typeof(Parser).Assembly.GetManifestResourceStream("UAParser.regexes.yaml"))
            // ReSharper disable once AssignNullToNotNullAttribute
            using (var reader = new StreamReader(stream))
                return new Parser(reader.ReadToEnd());
        }

        public ClientInfo Parse(string uaString)
        {
            var os     = ParseOS(uaString);
            var device = ParseDevice(uaString);
            var ua     = ParseUserAgent(uaString);
            return new ClientInfo(os, device, ua);
        }

        public OS ParseOS(string uaString) { return _osParser(uaString); }
        public Device ParseDevice(string uaString) { return _deviceParser(uaString); }
        public UserAgent ParseUserAgent(string uaString) { return _userAgentParser(uaString); }

        static IEnumerable<T> Read<T>(IEnumerable<YamlNode> nodes, Func<Func<string, string>, T> selector)
        {
            return from node in nodes ?? Enumerable.Empty<YamlNode>()
                   select node as YamlMappingNode into node
                   where node != null
                   select node.Children
                              .Where(e => e.Key is YamlScalarNode && e.Value is YamlScalarNode)
                              .GroupBy(e => e.Key.ToString(), e => e.Value.ToString(), StringComparer.OrdinalIgnoreCase)
                              .ToDictionary(e => e.Key, e => e.Last(), StringComparer.OrdinalIgnoreCase) into cm
                   select selector(cm.Find);
        }

        static Func<string, T> CreateParser<T>(IEnumerable<Func<string, T>> parsers, T defaultValue) where T : class
        {
            return CreateParser(parsers, defaultValue, t => t);
        }

        static Func<string, TResult> CreateParser<T, TResult>(IEnumerable<Func<string, T>> parsers, T defaultValue, Func<T, TResult> selector) where T : class
        {
            parsers = parsers != null ? parsers.ToArray() : Enumerable.Empty<Func<string, T>>();
            return ua => selector(parsers.Select(p => p(ua)).FirstOrDefault(m => m != null) ?? defaultValue);
        }

        static class Config
        {
            // ReSharper disable once InconsistentNaming
            public static Func<string, OS> OS(Func<string, string> indexer)
            {
                var regex = Regex(indexer, "OS");
                var os = indexer("os_replacement");
                var v1 = indexer("os_v1_replacement");
                var v2 = indexer("os_v2_replacement");
                return Parsers.OS(regex, os, v1, v2);
            }

            public static Func<string, UserAgent> UserAgent(Func<string, string> indexer)
            {
                var regex = Regex(indexer, "User agent");
                var family = indexer("family_replacement");
                var v1 = indexer("v1_replacement");
                var v2 = indexer("v2_replacement");
                return Parsers.UserAgent(regex, family, v1, v2);
            }

            public static Func<string, string> Device(Func<string, string> indexer)
            {
                return Parsers.Device(Regex(indexer, "Device"), indexer("device_replacement"));
            }

            static Regex Regex(Func<string, string> indexer, string key)
            {
                var pattern = indexer("regex");
                if (pattern == null)
                    throw new Exception(String.Format("{0} is missing regular expression specification.", key));

                // Some expressions in the regex.yaml file causes parsing errors 
                // in .NET such as the \_ token so need to alter them before 
                // proceeding.

                if (pattern.IndexOf(@"\_", StringComparison.Ordinal) >= 0)
                    pattern = pattern.Replace(@"\_", "_");

                // TODO: potentially allow parser to specify e.g. to use 
                // compiled regular expressions which are faster but increase 
                // startup time
                
                return new Regex(pattern);
            }
        }
        
        static class Parsers
        {
            // ReSharper disable once InconsistentNaming
            public static Func<string, OS> OS(Regex regex, string osReplacement, string v1Replacement, string v2Replacement)
            {
                return Regexer.Create(regex, p => p
                              .Replace(osReplacement, "$1", (_, f) => f)
                              .Replace(v1Replacement, (f, v1) => new { family = f, v1 })
                              .Replace(v2Replacement, (e, v2) => new { e.family, e.v1, v2 })
                              .Select((e, v3) => new { e.family, e.v1, e.v2, v3 })
                              .Select((e, v4) => new OS(e.family, e.v1, e.v2, e.v3, v4)));
            }

            public static Func<string, string> Device(Regex regex, string familyReplacement)
            {
                return Regexer.Create(regex, p => p.Replace(familyReplacement, "$1", (_, f) => f));
            }

            public static Func<string, UserAgent> UserAgent(Regex regex, string familyReplacement, string majorReplacement, string minorReplacement)
            {
                return Regexer.Create(regex, p => p
                              .Replace(familyReplacement, "$1", (_, f) => f)
                              .Replace(majorReplacement, (f, v1) => new { family = f, v1 })
                              .Replace(minorReplacement, (e, v2) => new { e.family, e.v1, v2 })
                              .Select((e, v3) => new UserAgent(e.family, e.v1, e.v2, v3)));
            }
        }
    }

    static class Regexer
    {
        public static Func<string, T> Create<T>(Regex regex, 
            Func<Func<Match, IEnumerator<int>, Match>, Func<Match, IEnumerator<int>, T>> builder)
        {
            var parser = builder((m, _) => m);
            return input =>
            {
                var m = regex.Match(input);
                var num = Generate(1, n => n + 1);
                return m.Success ? parser(m, num) : default(T);
            };
        }

        static IEnumerator<T> Generate<T>(T initial, Func<T, T> next)
        {
            for (var state = initial; ; state = next(state))
                yield return state;
            // ReSharper disable once FunctionNeverReturns
        }

        public static Func<Match, IEnumerator<int>, TResult> Replace<T, TResult>(
            this Func<Match, IEnumerator<int>, T> parser,
            string replacement,
            Func<T, string, TResult> resultSelector)
        {
            return replacement != null
                 ? parser.Select((a, _) => resultSelector(a, replacement))
                 : parser.Select(resultSelector);
        }

        public static Func<Match, IEnumerator<int>, TResult> Replace<T, TResult>(
            this Func<Match, IEnumerator<int>, T> parser,
            string replacement, string token,
            Func<T, string, TResult> resultSelector)
        {
            return replacement != null && replacement.Contains(token)
                 ? parser.Select((a, v) => resultSelector(a, v != null ? replacement.ReplaceFirstOccurence(token, v) : replacement))
                 : parser.Replace(replacement, resultSelector);
        }

        public static Func<Match, IEnumerator<int>, TResult> Select<T, TResult>(this Func<Match, IEnumerator<int>, T> parser, Func<T, string, TResult> resultSelector)
        {
            return parser.Then((m, num) =>
            {
                if (!num.MoveNext()) throw new InvalidOperationException();
                var groups = m.Groups;
                Group group;
                return num.Current <= groups.Count && (@group = groups[num.Current]).Success 
                     ? group.Value : null;
            }, resultSelector);
        }

        public static Func<Match, IEnumerator<int>, TResult> Then<TFirst, TSecond, TResult>(
            this Func<Match, IEnumerator<int>, TFirst> firstSelector,
            Func<Match, IEnumerator<int>, TSecond> secondSelector,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            return (m, num) => resultSelector(firstSelector(m, num), secondSelector(m, num));
        }
    }

    static class StringExtensions
    {
        public static string ReplaceFirstOccurence(this string input, string search, string replacement)
        {
            if (input == null) throw new ArgumentNullException("input");
            var index = input.IndexOf(search, StringComparison.Ordinal);
            return index >= 0
                 ? input.Substring(0, index) + replacement + input.Substring(index + search.Length)
                 : input;
        }
    }

    static class DictionaryExtensions
    {
        public static TValue Find<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : default(TValue);
        }
    }
}
