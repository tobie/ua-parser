using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Sdk;
using YamlDotNet.RepresentationModel;

namespace UAParser.Tests
{
  public class TestResourceTests
  {
    [Fact]
    public void can_run_device_tests()
    {
      RunTests<DeviceYamlTestCase>(
        "UAParser.Tests.TestResources.test_device.yaml",
        configMap => DeviceYamlTestCase.ReadFromMap(configMap));
    }

    [Fact]
    public void can_run_additional_os_tests()
    {
      RunTests<OSYamlTestCase>(
        "UAParser.Tests.TestResources.additional_os_tests.yaml",
        configMap => OSYamlTestCase.ReadFromMap(configMap));
    }

    [Fact]
    public void can_run_firefox_user_agent_string_tests()
    {
      RunTests<UserAgentYamlTestCase>(
        "UAParser.Tests.TestResources.firefox_user_agent_strings.yaml",
        configMap => UserAgentYamlTestCase.ReadFromMap(configMap));
    }

    [Fact]
    public void can_run_pgts_browser_list_tests()
    {
      RunTests<UserAgentYamlTestCase>(
        "UAParser.Tests.TestResources.pgts_browser_list.yaml",
        configMap => UserAgentYamlTestCase.ReadFromMap(configMap));
    }

    [Fact]
    public void can_run_user_agent_parser_tests()
    {
      RunTests<UserAgentYamlTestCase>(
        "UAParser.Tests.TestResources.test_user_agent_parser.yaml",
        configMap => UserAgentYamlTestCase.ReadFromMap(configMap));
    }

    [Fact]
    public void can_run_user_agent_parser_os_tests()
    {
      RunTests<OSYamlTestCase>(
        "UAParser.Tests.TestResources.test_user_agent_parser_os.yaml",
        configMap => OSYamlTestCase.ReadFromMap(configMap));
    }

    public static void RunTests<TTestCase>(
      string resourceName,
      Func<Dictionary<string, string>, TTestCase> testCaseFunction) where TTestCase : YamlTestCase
    {
      List<TTestCase> testCases = GetTestCases<TTestCase>(
        resourceName,
        "test_cases",
        testCaseFunction);

      RunTestCases<TTestCase>(testCases);
    }

    private static void RunTestCases<TTestCase>(List<TTestCase> testCases) where TTestCase : YamlTestCase
    {
      Parser parser = Parser.GetDefault();

      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < testCases.Count; i++)
      {
        var tc = testCases[i];
        if (tc == null)
          continue;

        var clientInfo = parser.Parse(tc.UserAgent);
        try
        {
          tc.Verify(clientInfo);
        }
        catch (AssertException ex)
        {
          sb.AppendLine("testcase "+(i+1)+": " +ex.Message);
        }
      }

      Console.WriteLine("Ran {0} tests", testCases.Count);
      Assert.True(0 == sb.Length, "Failed tests: " + Environment.NewLine + sb.ToString());
    }

    public static List<TTestCase> GetTestCases<TTestCase>(
  string resourceName,
  string yamlNodeName,
  Func<Dictionary<string, string>, TTestCase> testCaseFunction)
    {
      string yamlContent = GetTestResources(resourceName);
      YamlStream yaml = new YamlStream();
      yaml.Load(new StringReader(yamlContent));

      //reading overall configurations
      var regexConfigNode = (YamlMappingNode)yaml.Documents[0].RootNode;
      var regexConfig = new Dictionary<string, YamlNode>();
      foreach (var entry in regexConfigNode.Children)
      {
        regexConfig.Add(((YamlScalarNode)entry.Key).Value, entry.Value);
      }

      var testCasesNode = (YamlSequenceNode)regexConfig[yamlNodeName];
      List<TTestCase> testCases = testCasesNode.ConvertToDictionaryList()
        .Select(configMap =>
        {
          if (!configMap.ContainsKey("js_ua")) //we deliberatly skip tests with js-user agents
            return testCaseFunction(configMap);
          return default(TTestCase);
        })
        .ToList();
      return testCases;
    }

    public static string GetTestResources(string name)
    {
      using (Stream s = typeof(TestResourceTests).Assembly.GetManifestResourceStream(name))
      {
        if (s == null)
          throw new InvalidOperationException("Could not locate an embedded test resource with name: " + name);
        using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
        {
          return sr.ReadToEnd();
        }
      }
    }
  }
}
