using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace UAParser
{
    public class Parser
    {
        private OSParser m_osParser;
        private DeviceParser m_deviceParser;
        private UserAgentParser m_userAgentParser;

        protected Parser(string rawYaml)
        {
            ReadYaml(rawYaml);
        }

        public static Parser FromRawYaml(string rawYaml)
        {
            return new Parser(rawYaml);
        }
        public static Parser FromYamlFile(string pathToYamlFile)
        {
            string rawYaml = File.ReadAllText(pathToYamlFile);
            return new Parser(rawYaml);
        }
        public static Parser GetDefault()
        {
            Stream stream = typeof(Parser).Assembly.GetManifestResourceStream("UAParser.regexes.yaml");
            string content = new StreamReader(stream).ReadToEnd();
            return new Parser(content);
        }

        public ClientInfo Parse(String agentString)
        {
            UserAgent ua = ParseUserAgent(agentString);
            OS os = ParseOS(agentString);
            Device device = m_deviceParser.ParseUserAgentString(agentString, (ua == null ? null : ua.Family));
            return new ClientInfo(os, device, ua);
        }

        public UserAgent ParseUserAgent(String agentString)
        {
            return m_userAgentParser.ParseAgentString(agentString);
        }

        public Device ParseDevice(String agentString)
        {
            return m_deviceParser.ParseUserAgentString(agentString);
        }

        public OS ParseOS(String agentString)
        {
            return m_osParser.ParseUserAgentString(agentString);
        }

        private void ReadYaml(string rawYaml)
        {
            using (StringReader reader = new StringReader(rawYaml))
            {
                YamlStream yaml = new YamlStream();
                yaml.Load(reader);

                //reading overall configurations
                var regexConfigNode = (YamlMappingNode)yaml.Documents[0].RootNode;
                var regexConfig = new Dictionary<string, YamlNode>();
                foreach (var entry in regexConfigNode.Children)
                {
                    regexConfig.Add(((YamlScalarNode)entry.Key).Value, entry.Value);
                }

                //user agents regex
                var uaParserConfigs = (YamlSequenceNode)regexConfig["user_agent_parsers"];
                uaParserConfigs.ThrowIfNull("user_agent_parsers is missing from yaml");
                List<UserAgentPattern> userAgentPatterns = uaParserConfigs.ConvertToDictionaryList().Select(configMap => YamlParsing.UserAgentPatternFromMap(configMap)).ToList();
                m_userAgentParser = new UserAgentParser(userAgentPatterns);

                // operating system regex
                var osParserConfigs = (YamlSequenceNode)regexConfig["os_parsers"];
                osParserConfigs.ThrowIfNull("os_parsers is missing from yaml");
                List<OSPattern> osPatterns = osParserConfigs.ConvertToDictionaryList().Select(configMap => YamlParsing.OSPatternFromMap(configMap)).ToList();
                m_osParser = new OSParser(osPatterns);

                // device parser setup
                var deviceParserConfigs = (YamlSequenceNode)regexConfig["device_parsers"];
                deviceParserConfigs.ThrowIfNull("device_parsers is missing from yaml");
                var mobileUAFamiliesList = (YamlSequenceNode)regexConfig["mobile_user_agent_families"];
                var mobileOSFamiliesList = (YamlSequenceNode)regexConfig["mobile_os_families"];
                List<string> mobileUAFamilies = (new List<string>(mobileUAFamiliesList.Select(f => f.ToString())));
                List<string> mobileOSFamilies = (new List<string>(mobileOSFamiliesList.Select(f => f.ToString())));

                List<DevicePattern> devicePatterns = deviceParserConfigs.ConvertToDictionaryList().Select(configMap => YamlParsing.DevicePatternFromMap(configMap)).ToList();
                m_deviceParser = new DeviceParser( devicePatterns, m_userAgentParser, mobileUAFamilies, mobileOSFamilies);
            }
        }
    }
}
