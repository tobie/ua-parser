package ua_parser;

import java.io.InputStream;
import java.util.List;
import java.util.Map;

import org.junit.Test;
import org.junit.Before;
import static org.junit.Assert.*;

import org.yaml.snakeyaml.Yaml;

public class ParserTest {
  final String TEST_RESOURCE_PATH = "/ua_parser/";
  Yaml yaml = new Yaml();
  Parser parser;

  @Before
  public void initParser() throws Exception {
    parser = new Parser();
  }

  @Test
  public void testParseUserAgent() {
    testUserAgentFromYaml("test_user_agent_parser.yaml");
  }

  @Test
  public void testParseOS() {
    testOSFromYaml("test_user_agent_parser_os.yaml");
  }

  @Test
  public void testParseAdditionalOS() {
    testOSFromYaml("additional_os_tests.yaml");
  }


  @Test
  public void testParseDevice() {
    testDeviceFromYaml("test_device.yaml");
  }

  @Test
  public void testParseFirefox() {
    testUserAgentFromYaml("firefox_user_agent_strings.yaml");
  }

  @Test
  public void testParsePGTS() {
    testUserAgentFromYaml("pgts_browser_list.yaml");
  }


  @Test
  public void testParseAll() {
    String agentString = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.4; fr; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5,gzip(gfe),gzip(gfe)";

    assertEquals(parser.parseUserAgent(agentString), new UserAgent("Firefox", "3", "5", "5"));
    assertEquals(parser.parseOS(agentString), new OS("Mac OS X", "10", "4", null, null));
    assertEquals(parser.parseDevice(agentString), new Device(null, false, false));
  }

  void testUserAgentFromYaml(String filename) {
    InputStream yamlStream = this.getClass().getResourceAsStream(TEST_RESOURCE_PATH + filename);

    List<Map> testCases = (List<Map>) ((Map)yaml.load(yamlStream)).get("test_cases");
    for(Map<String, String> testCase : testCases) {
      // Skip tests with js_ua as those overrides are not yet supported in java
      if (testCase.containsKey("js_ua")) continue;

      assertEquals(UserAgent.fromMap(testCase), parser.parseUserAgent(testCase.get("user_agent_string")));
    }
  }

  void testOSFromYaml(String filename) {
    InputStream yamlStream = this.getClass().getResourceAsStream(TEST_RESOURCE_PATH + filename);

    List<Map> testCases = (List<Map>) ((Map)yaml.load(yamlStream)).get("test_cases");
    for(Map<String, String> testCase : testCases) {
      // Skip tests with js_ua as those overrides are not yet supported in java
      if (testCase.containsKey("js_ua")) continue;

      assertEquals(OS.fromMap(testCase), parser.parseOS(testCase.get("user_agent_string")));
    }
  }

  void testDeviceFromYaml(String filename) {
    InputStream yamlStream = this.getClass().getResourceAsStream(TEST_RESOURCE_PATH + filename);

    List<Map> testCases = (List<Map>) ((Map)yaml.load(yamlStream)).get("test_cases");
    for(Map<String, Object> testCase : testCases) {
      assertEquals(Device.fromMap(testCase), parser.parseDevice((String)testCase.get("user_agent_string")));
    }
  }
}
