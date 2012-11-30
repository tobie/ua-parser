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
    /// Operating System parser using ua-parser. Extracts OS information from user agent strings.
    /// </summary>
    public class OSParser
    {
        private List<OSPattern> patterns;

        public OSParser(List<OSPattern> patterns)
        {
            this.patterns = patterns;
        }

        public static OSParser FromList(List<Dictionary<String, String>> configList)
        {
            List<OSPattern> configPatterns = new List<OSPattern>();

            foreach (Dictionary<String, String> configMap in configList)
            {
                configPatterns.Add(OSParser.PatternFromMap(configMap));
            }
            return new OSParser(configPatterns);
        }

        public OS Parse(String agentString)
        {
            OS os;
            foreach (OSPattern p in patterns)
            {
                if ((os = p.Match(agentString)) != null)
                {
                    return os;
                }
            }
            return new OS("Other", null, null, null, null);
        }

        protected static OSPattern PatternFromMap(IDictionary<String, String> configMap)
        {
            String regex = configMap["regex"];
            if (regex == null)
            {
                throw new ArgumentException("OS is missing regex");
            }

            string os = null, v1 = null, v2 = null;

            configMap.TryGetValue("os_replacement", out os);
            configMap.TryGetValue("os_v1_replacement", out v1);
            configMap.TryGetValue("os_v2_replacement", out v2);

            return (new OSPattern(new Regex(regex),
                                 os, v1, v2));
        }

        public class OSPattern
        {
            private Regex pattern;
            private String osReplacement, v1Replacement, v2Replacement;

            public OSPattern(Regex pattern, String osReplacement, String v1Replacement, String v2Replacement)
            {
                this.pattern = pattern;
                this.osReplacement = osReplacement;
                this.v1Replacement = v1Replacement;
                this.v2Replacement = v2Replacement;
            }

            public OS Match(String agentString)
            {
                String family = null, v1 = null, v2 = null, v3 = null, v4 = null;
                var match = pattern.Match(agentString);

                if (match.Length == 0)
                {
                    return null;
                }

                int groupCount = match.Groups.Count;

                if (osReplacement != null)
                {
                    family = osReplacement;
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
                            if (groupCount >= 5)
                            {
                                v4 = match.Groups[5].Value;
                            }
                        }
                    }
                }
                return family == null ? null : new OS(family, v1, v2, v3, v4);
            }
        }
    }
}
