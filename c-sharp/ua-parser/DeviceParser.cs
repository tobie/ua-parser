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
using System.Text.RegularExpressions;

namespace ua_parser
{
    /// <summary>
    /// Device parser using ua-parser regexes. Extracts device information from user agent strings.
    /// </summary>
    public class DeviceParser
    {

        List<DevicePattern> patterns;
        private List<String> mobileUAFamilies, mobileOSFamilies;
        private UserAgentParser uaParser;

        public DeviceParser(List<DevicePattern> patterns, UserAgentParser uaParser,
                            List<String> mobileUAFamilies, List<String> mobileOSFamilies)
        {
            this.patterns = patterns;
            this.uaParser = uaParser;
            this.mobileUAFamilies = mobileUAFamilies;
            this.mobileOSFamilies = mobileOSFamilies;
        }

        public Device Parse(String agentString)
        {
            return Parse(agentString, uaParser.Parse(agentString).family);
        }

        public Device Parse(String agentString, String userAgentFamily)
        {
            String device = null;
            foreach (DevicePattern p in patterns)
            {
                if ((device = p.Match(agentString)) != null)
                {
                    break;
                }
            }

            String osFamily = device == null ? "Other" : device;
            userAgentFamily = userAgentFamily == null ? "Other" : userAgentFamily;

            return new Device(device,
                              mobileUAFamilies.Contains(userAgentFamily) || mobileOSFamilies.Contains(osFamily),
                              (device != null && device.Equals("Spider")));
        }

        public static DeviceParser FromList(List<Dictionary<String, String>> configList, UserAgentParser uaParser, List<String> mobileUAFamilies, List<String> mobileOSFamilies)
        {
            List<DevicePattern> configPatterns = new List<DevicePattern>();
            foreach (var configMap in configList)
            {
                configPatterns.Add(DeviceParser.PatternFromMap(configMap));
            }
            return new DeviceParser(configPatterns, uaParser, mobileUAFamilies, mobileOSFamilies);
        }

        protected static DevicePattern PatternFromMap(IDictionary<String, String> configMap)
        {
            Regex regex = new Regex(configMap["regex"]);

            if (regex == null)
            {
                throw new ArgumentException("Device is missing regex");
            }

            string device = null;

            configMap.TryGetValue("device_replacement", out device);

            return new DevicePattern(regex, device);
        }

        public class DevicePattern
        {
            private Regex pattern;
            private String familyReplacement;

            public DevicePattern(Regex pattern, String familyReplacement)
            {
                this.pattern = pattern;
                this.familyReplacement = familyReplacement;
            }

            public String Match(String agentString)
            {
                var match = pattern.Match(agentString);

                if (match.Length == 0)
                {
                    return null;
                }

                String family = null;

                if (familyReplacement != null)
                {
                    if (familyReplacement.Contains("$1") && match.Groups.Count >= 1 && match.Groups[1] != null)
                    {
                        family = familyReplacement.ReplaceFirst("\\$1", Regex.Escape(match.Groups[1].Value));
                    }
                    else
                    {
                        family = familyReplacement;
                    }
                }
                else if (match.Groups.Count >= 1)
                {
                    family = match.Groups[1].Value;
                }
                return family;
            }
        }
    }
}
