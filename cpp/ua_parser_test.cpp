/*
# Copyright 2013 Andrew Punch
#
# Licensed under the Apache License, Version 2.0 (the 'License')
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#         http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an 'AS IS' BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
*/

#include <string>
#include <fstream>
#include <gtest/gtest.h>
#include "ua_parser.h"

static const std::string TEST_RESOURCES_DIR="../test_resources/";

class UaParserTest : public testing::Test {
 protected:
  bool yaml_isnull(const YAML::Node &node) {
    return node.Type()==YAML::NodeType::Null;
  }

  void runUserAgentTestsFromYAML(const std::string &file_name) {
    std::ifstream in(file_name.c_str());
    if (!in.good()) {
      FAIL() << "Could not open YAML file" << file_name;
      return;
    }

    YAML::Parser parser(in);

    YAML::Node doc;
    parser.GetNextDocument(doc);

    const YAML::Node &test_cases = doc["test_cases"];
    for (YAML::Iterator i=test_cases.begin(); i!=test_cases.end(); i++) {
      // Inputs to Parse()
      const YAML::Node& test_case = *i;
      std::string user_agent_string = test_case["user_agent_string"].to<std::string>();

      // The expected results
      ua_parser::Browser expected;
      if (!yaml_isnull(test_case["family"])) expected.family = test_case["family"].to<std::string>();
      if (!yaml_isnull(test_case["major"]))  expected.major  = test_case["major"].to<std::string>();
      if (!yaml_isnull(test_case["minor"]))  expected.minor  = test_case["minor"].to<std::string>();
      if (!yaml_isnull(test_case["patch"]))  expected.patch  = test_case["patch"].to<std::string>();

      // js_ua not supported
      if (test_case.FindValue("js_ua"))
        continue;

      ua_parser::Parser user_agent_parser("../regexes.yaml");
      ua_parser::UserAgent result = user_agent_parser.Parse(user_agent_string);
      EXPECT_EQ(expected, result.browser)<<user_agent_string;
    }
  }

  void runOSTestsFromYAML(const std::string &file_name) {
    std::ifstream in(file_name.c_str());
    if (!in.good()) {
      FAIL() << "Could not open YAML file" << file_name;
      return;
    }

    YAML::Parser parser(in);

    YAML::Node doc;
    parser.GetNextDocument(doc);

    const YAML::Node &test_cases = doc["test_cases"];

    for (YAML::Iterator i=test_cases.begin(); i!=test_cases.end(); i++) {
      // Inputs to Parse()
      const YAML::Node &test_case = *i;
      std::string user_agent_string = test_case["user_agent_string"].to<std::string>();

      // The expected results
      ua_parser::OperatingSystem expected;
      if (!yaml_isnull(test_case["family"])) expected.os = test_case["family"].to<std::string>();
      if (!yaml_isnull(test_case["major"]))  expected.major  = test_case["major"].to<std::string>();
      if (!yaml_isnull(test_case["minor"]))  expected.minor  = test_case["minor"].to<std::string>();
      if (!yaml_isnull(test_case["patch"]))  expected.patch  = test_case["patch"].to<std::string>();
      if (!yaml_isnull(test_case["patch_minor"]))  expected.patch_minor = test_case["patch_minor"].to<std::string>();

      ua_parser::Parser user_agent_parser("../regexes.yaml");
      ua_parser::UserAgent result = user_agent_parser.Parse(user_agent_string);
      EXPECT_EQ(result.os, expected) << user_agent_string;
    }
  }

  void runDeviceTestsFromYAML(const std::string &file_name) {
    std::ifstream in(file_name.c_str());
    if (!in.good()) {
      FAIL() << "Could not open YAML file" << file_name;
      return;
    }

    YAML::Parser parser(in);

    YAML::Node doc;
    parser.GetNextDocument(doc);

    const YAML::Node &test_cases = doc["test_cases"];

    for (YAML::Iterator i=test_cases.begin(); i!=test_cases.end(); i++) {
      const YAML::Node &test_case = *i;
      // Inputs to Parse()
      std::string user_agent_string = test_case["user_agent_string"].to<std::string>();

      // The expected results
      ua_parser::Device expected = test_case["family"].to<std::string>();

      ua_parser::Parser user_agent_parser("../regexes.yaml");
      ua_parser::UserAgent result = user_agent_parser.Parse(user_agent_string);
      EXPECT_EQ(result.device, expected)<<user_agent_string;
    }
  }



};


TEST_F(UaParserTest, TestBrowserscopeStrings) {
  runUserAgentTestsFromYAML(
    TEST_RESOURCES_DIR+"test_user_agent_parser.yaml");
}


TEST_F(UaParserTest, TestBrowserscopeStringsOS) {
  runOSTestsFromYAML(
    TEST_RESOURCES_DIR+"test_user_agent_parser_os.yaml");
}

TEST_F(UaParserTest, TestStringsOS) {
  runOSTestsFromYAML(
    TEST_RESOURCES_DIR+"additional_os_tests.yaml");
}

TEST_F(UaParserTest, TestStringsDevice) {
  runDeviceTestsFromYAML(
    TEST_RESOURCES_DIR+"test_device.yaml");
}

TEST_F(UaParserTest, TestMozillaStrings) {
  runUserAgentTestsFromYAML(
    TEST_RESOURCES_DIR+"firefox_user_agent_strings.yaml");
}


int main(int argc, char **argv) {
  ::testing::InitGoogleTest(&argc, argv);
  return RUN_ALL_TESTS();
}
