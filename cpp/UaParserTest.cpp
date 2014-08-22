#include "UaParser.h"
#include <fstream>
#include <glog/logging.h>
#include <gtest/gtest.h>
#include <yaml-cpp/yaml.h>
#include <string>

namespace {

const UserAgentParser g_ua_parser("../regexes.yaml");

TEST(UserAgentParser, basic) {
  const auto uagent = g_ua_parser.parse(
    "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 "
    "(KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3");
  ASSERT_EQ("Mobile Safari", uagent.browser.family);
  ASSERT_EQ("5", uagent.browser.major);
  ASSERT_EQ("1", uagent.browser.minor);
  ASSERT_EQ("", uagent.browser.patch);
  ASSERT_EQ("Mobile Safari 5.1.0", uagent.browser.toString());
  ASSERT_EQ("5.1.0", uagent.browser.toVersionString());

  ASSERT_EQ("iOS", uagent.os.family);
  ASSERT_EQ("5", uagent.os.major);
  ASSERT_EQ("1", uagent.os.minor);
  ASSERT_EQ("1", uagent.os.patch);
  ASSERT_EQ("iOS 5.1.1", uagent.os.toString());
  ASSERT_EQ("5.1.1", uagent.os.toVersionString());

  ASSERT_EQ("Mobile Safari 5.1.0/iOS 5.1.1", uagent.toFullString());

  ASSERT_EQ("iPhone", uagent.device.family);

  ASSERT_FALSE(uagent.isSpider());
}

namespace {

std::string string_field(const YAML::Node& root, const std::string& fname) {
  const auto& yaml_field = root[fname];
  return YAML::IsNull(yaml_field) ? "" : yaml_field.to<std::string>();
}

void test_browser_or_os(const char* file_path, const bool browser) {
  std::ifstream in_stream(file_path);
  CHECK(in_stream.good());
  YAML::Parser yaml_parser(in_stream);
  YAML::Node root;
  CHECK(yaml_parser.GetNextDocument(root));
  const auto& test_cases = root["test_cases"];
  for (const auto& test : test_cases) {
    // TODO(alex): add support for JS user agent
    if (test.FindValue("js_ua")) {
      continue;
    }
    const auto major = string_field(test, "major");
    const auto minor = string_field(test, "minor");
    const auto patch = string_field(test, "patch");
    const auto family = string_field(test, "family");
    const auto unparsed = string_field(test, "user_agent_string");
    const auto uagent = g_ua_parser.parse(unparsed);
    const auto& agent = browser ? uagent.browser : uagent.os;
    ASSERT_EQ(major, agent.major);
    ASSERT_EQ(minor, agent.minor);
    ASSERT_EQ(patch, agent.patch);
    ASSERT_EQ(family, agent.family);
  }
}

void test_device(const char* file_path) {
  std::ifstream in_stream(file_path);
  CHECK(in_stream.good());
  YAML::Parser yaml_parser(in_stream);
  YAML::Node root;
  CHECK(yaml_parser.GetNextDocument(root));
  const auto& test_cases = root["test_cases"];
  for (const auto& test : test_cases) {
    const auto unparsed = string_field(test, "user_agent_string");
    const auto uagent = g_ua_parser.parse(unparsed);
    const auto family = string_field(test, "family");
    ASSERT_EQ(family, uagent.device.family);
  }
}

}  // namespace

TEST(BrowserVersion, test_user_agent_parser) {
  test_browser_or_os("../test_resources/test_user_agent_parser.yaml", true);
}

TEST(BrowserVersion, firefox_user_agent_strings) {
  test_browser_or_os("../test_resources/firefox_user_agent_strings.yaml", true);
}

TEST(BrowserVersion, pgts_browser_list) {
  test_browser_or_os("../test_resources/pgts_browser_list.yaml", true);
}

TEST(OsVersion, test_user_agent_parser_os) {
  test_browser_or_os("../test_resources/test_user_agent_parser_os.yaml", false);
}

TEST(OsVersion, additional_os_tests) {
  test_browser_or_os("../test_resources/additional_os_tests.yaml", false);
}

TEST(DeviceFamily, test_device) {
  test_device("../test_resources/test_device.yaml");
}

}  // namespace

int main(int argc, char** argv) {
  testing::InitGoogleTest(&argc, argv);
  return RUN_ALL_TESTS();
}
