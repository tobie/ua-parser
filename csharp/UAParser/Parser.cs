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
  /// <summary>
  /// Represents a parser for user agent strings
  /// </summary>
  public class Parser
  {
    private OSParser m_osParser;
    private DeviceParser m_deviceParser;
    private UserAgentParser m_userAgentParser;

    internal Parser(string rawYaml)
    {
      ReadYaml(rawYaml);
    }

    /// <summary>
    /// Construct a parser from a raw yaml string containing the regular expressions
    /// defining the user agent information
    /// </summary>
    /// <remarks>Use this static method if you need to use modified or updated
    /// regular expressions</remarks>
    /// <param name="rawYaml">the raw yaml string</param>
    /// <returns>A parser for the defined regular expressions</returns>
    public static Parser FromRawYaml(string rawYaml)
    {
      return new Parser(rawYaml);
    }
    /// <summary>
    /// Construct a parser from a named file containing the regular expressions
    /// defining the user agent information
    /// </summary>
    /// <remarks>Use this static method if you need to use modified or updated
    /// regular expressions</remarks>
    /// <param name="pathToYamlFile">The absolute path to the yaml file with regular expressions</param>
    /// <returns></returns>
    public static Parser FromYamlFile(string pathToYamlFile)
    {
      string rawYaml = File.ReadAllText(pathToYamlFile);
      return new Parser(rawYaml);
    }
    /// <summary>
    /// Get a parser for the default regular expressions contained in the
    /// assembly.
    /// </summary>
    /// <remarks>You should use this static method unless you explicitly 
    /// need to use modified or updated regular expressions</remarks>
    /// <returns></returns>
    public static Parser GetDefault()
    {
      Stream stream = typeof(Parser).Assembly.GetManifestResourceStream("UAParser.regexes.yaml");
      string content = new StreamReader(stream).ReadToEnd();
      return new Parser(content);
    }

    /// <summary>
    /// Parse a user agent string to the full client information
    /// </summary>
    /// <param name="agentString">the user agent string</param>
    /// <returns>a <see cref="ClientInfo"/> instance</returns>
    public ClientInfo Parse(String agentString)
    {
      UserAgent ua = ParseUserAgent(agentString);
      OS os = ParseOS(agentString);
      Device device = m_deviceParser.ParseUserAgentString(agentString, (ua == null ? null : ua.Family));
      return new ClientInfo(os, device, ua);
    }
    /// <summary>
    /// Parse a user agent string to the user agent information
    /// </summary>
    /// <param name="agentString">the user agent string</param>
    /// <returns>a <see cref="UserAgent"/> instance</returns>
    public UserAgent ParseUserAgent(String agentString)
    {
      return m_userAgentParser.ParseAgentString(agentString);
    }
    /// <summary>
    /// Parse a user agent string to the device information
    /// </summary>
    /// <param name="agentString">the user agent string</param>
    /// <returns>a <see cref="Device"/> instance</returns>
    public Device ParseDevice(String agentString)
    {
      return m_deviceParser.ParseUserAgentString(agentString);
    }
    /// <summary>
    /// Parse a user agent string to the operating system information
    /// </summary>
    /// <param name="agentString">the user agent string</param>
    /// <returns>a <see cref="OS"/> instance</returns>
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
        List<DevicePattern> devicePatterns = deviceParserConfigs.ConvertToDictionaryList().Select(configMap => YamlParsing.DevicePatternFromMap(configMap)).ToList();
        m_deviceParser = new DeviceParser(devicePatterns, m_userAgentParser);
      }
    }
  }
}
