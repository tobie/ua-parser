/**
 * Copyright 2012 viagogo AG
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ua_parser
{
    /// <summary>
    /// User Agent parser using ua-parser regexes
    /// </summary>
    public class UserAgentParser
    {
        private List<UAPattern> patterns;

        public UserAgentParser(List<UAPattern> patterns)
        {
            this.patterns = patterns;
        }

        public static UserAgentParser FromList(List<Dictionary<String, String>> configList)
        {
            List<UAPattern> configPatterns = new List<UAPattern>();

            foreach (Dictionary<String, String> configMap in configList)
            {
                configPatterns.Add(UserAgentParser.PatternFromMap(configMap));
            }
            return new UserAgentParser(configPatterns);
        }

        public UserAgent Parse(String agentString)
        {
            UserAgent agent;
            foreach (UAPattern p in patterns)
            {
                if ((agent = p.Match(agentString)) != null)
                {
                    return agent;
                }
            }
            return new UserAgent("Other", null, null, null);
        }

        protected static UAPattern PatternFromMap(IDictionary<String, String> configMap)
        {
            String regex = configMap["regex"];
            if (regex == null)
            {
                throw new ArgumentException("User agent is missing regex");
            }

            string family = null, v1 = null, v2 = null;

            configMap.TryGetValue("family_replacement", out family);
            configMap.TryGetValue("v1_replacement", out v1);
            configMap.TryGetValue("v2_replacement", out v2);

            return (new UAPattern(new Regex(regex),
                                 family, v1, v2));
        }

        public class UAPattern
        {
            private Regex pattern;
            private String familyReplacement, v1Replacement, v2Replacement;

            public UAPattern(Regex pattern, String familyReplacement, String v1Replacement, String v2Replacement)
            {
                this.pattern = pattern;
                this.familyReplacement = familyReplacement;
                this.v1Replacement = v1Replacement;
                this.v2Replacement = v2Replacement;
            }

            public UserAgent Match(String agentString)
            {
                String family = null, v1 = null, v2 = null, v3 = null;
                var match = pattern.Match(agentString);

                if (match.Length == 0)
                {
                    return null;
                }

                int groupCount = match.Groups.Count;

                if (familyReplacement != null)
                {
                    if (familyReplacement.Contains("$1") && groupCount >= 1 && match.Groups[1] != null)
                    {
                        family = familyReplacement.ReplaceFirst("\\$1", Regex.Escape(match.Groups[1].Value));
                    }
                    else
                    {
                        family = familyReplacement;
                    }
                }
                else if (groupCount >= 1)
                {
                    family = match.Groups[1].Value;
                }

                if (v1Replacement != null)
                {
                    v1 = v1Replacement;
                }
                else if (groupCount >= 2)
                {
                    v1 = match.Groups[2].Value;
                }

                if (v2Replacement != null)
                {
                    v2 = v2Replacement;
                }
                else if (groupCount >= 3)
                {
                    v2 = match.Groups[3].Value;
                    if (groupCount >= 4)
                    {
                        v3 = match.Groups[4].Value;
                    }
                }
                return family == null ? null : new UserAgent(family, v1, v2, v3);
            }
        }
    }
}
