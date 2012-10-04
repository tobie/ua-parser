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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace ua_parser
{
    /// <summary>
    /// C# implementation of <a href="https://github.com/tobie/ua-parser">UA Parser</a>
    /// </summary>
    public class Parser
    {
        private static String REGEX_YAML_PATH = "regexes.yaml";
        private UserAgentParser uaParser;
        private OSParser osParser;
        private DeviceParser deviceParser;

        public Parser()
            : this(new FileStream(Parser.REGEX_YAML_PATH, FileMode.Open))
        {
        }

        public Parser(Stream regexYaml)
        {
            Initialize(regexYaml);
        }

        public Client Parse(String agentString)
        {
            UserAgent ua = ParseUserAgent(agentString);
            OS os = ParseOS(agentString);
            Device device = deviceParser.Parse(agentString, (ua == null ? null : ua.family));
            return new Client(ua, os, device);
        }

        public UserAgent ParseUserAgent(String agentString)
        {
            return uaParser.Parse(agentString);
        }

        public Device ParseDevice(String agentString)
        {
            return deviceParser.Parse(agentString);
        }

        public OS ParseOS(String agentString)
        {
            return osParser.Parse(agentString);
        }

        private void Initialize(Stream regexYaml)
        {
            TextReader input = new StreamReader(regexYaml);
            YamlStream yaml = new YamlStream();
            yaml.Load(input);

            var regexConfigNode = (YamlMappingNode)yaml.Documents[0].RootNode;
            var regexConfig = new Dictionary<String, YamlNode>();
            foreach (var entry in regexConfigNode.Children)
            {
                regexConfig.Add(((YamlScalarNode)entry.Key).Value, entry.Value);
            }

            var uaParserConfigs = (YamlSequenceNode)regexConfig["user_agent_parsers"];
            if (uaParserConfigs == null)
            {
                throw new ArgumentException("user_agent_parsers is missing from yaml");
            }
            uaParser = UserAgentParser.FromList(uaParserConfigs.ToListOfDictionaries());


            var osParserConfigs = (YamlSequenceNode)regexConfig["os_parsers"];
            if (osParserConfigs == null)
            {
                throw new ArgumentException("os_parsers is missing from yaml");
            }
            osParser = OSParser.FromList(osParserConfigs.ToListOfDictionaries());

            var deviceParserConfigs = (YamlSequenceNode)regexConfig["device_parsers"];
            if (deviceParserConfigs == null)
            {
                throw new ArgumentException("device_parsers is missing from yaml");
            }

            var mobileUAFamiliesList = (YamlSequenceNode)regexConfig["mobile_user_agent_families"];
            var mobileOSFamiliesList = (YamlSequenceNode)regexConfig["mobile_os_families"];

            List<String> mobileUAFamilies = (new List<String>(mobileUAFamiliesList.Select(f => f.ToString())));
            List<String> mobileOSFamilies = (new List<String>(mobileOSFamiliesList.Select(f => f.ToString())));

            deviceParser = DeviceParser.FromList(deviceParserConfigs.ToListOfDictionaries(), uaParser, mobileUAFamilies, mobileOSFamilies);
        }
    }
}
