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

#ifndef __UA_PARSER_H__
#define __UA_PARSER_H__

#include <string>
#include <vector>
#include <boost/regex.hpp>
#include <yaml-cpp/yaml.h>

namespace ua_parser {
class Browser {
 public:
  std::string family;
  std::string major;
  std::string minor;
  std::string patch;
  std::string patch_minor;

  bool operator==(const Browser &rhs) const {
    return family==rhs.family
           && major==rhs.major
           && minor==rhs.minor
           && patch==rhs.patch
           && patch_minor==rhs.patch_minor;
  }


};


inline std::ostream& operator<<(std::ostream &o, const Browser &browser) {
  return o <<"Browser: ("
         << browser.family<<","
         <<browser.major<<","
         <<browser.minor<<","
         <<browser.patch<<","
         <<browser.patch_minor<<") ";
}


class OperatingSystem {
 public:
  std::string os;
  std::string major;
  std::string minor;
  std::string patch;
  std::string patch_minor;

  bool operator==(const OperatingSystem &rhs) const {
    return os==rhs.os
           && major==rhs.major
           && minor==rhs.minor
           && patch==rhs.patch
           && patch_minor==rhs.patch_minor;
  }
};

inline std::ostream& operator<<(std::ostream &o, const OperatingSystem &os) {
  return o<<"OS: ("
         <<os.os<<","
         <<os.major<<","
         <<os.minor<<","
         <<os.patch<<","
         <<os.patch_minor<<") ";
}


typedef std::string Device;

class BrowserParser {
 public:
  BrowserParser(const YAML::Node &yaml_attributes);
  Browser Parse(const std::string &user_agent_string);

  std::string pattern;
  boost::regex regex;
  Browser overrides;
};


class OperatingSystemParser {
 public:
  OperatingSystemParser(const YAML::Node &yaml_attributes);
  OperatingSystem Parse(const std::string &user_agent_string);

  std::string pattern;
  boost::regex regex;
  OperatingSystem overrides;
};

class DeviceParser {
 public:
  DeviceParser(const YAML::Node &yaml_attributes);
  Device Parse(const std::string &user_agent_string);
  std::string pattern;
  boost::regex regex;
  Device overrides;
};


class UserAgent {
 public:
  Browser browser;
  OperatingSystem os;
  Device device;
  std::string user_agent_string;

};

inline std::ostream& operator<<(std::ostream &o, UserAgent &ua) {
  return o  <<ua.browser
         <<ua.os
         <<"Device: ("<<ua.device<<")";
}


class Parser {
 public:
  Parser(const std::string &yaml_file);
  UserAgent Parse(const std::string &user_agent_string);

 private:
  std::vector<BrowserParser> _browser_parsers;
  std::vector<OperatingSystemParser> _os_parsers;
  std::vector<DeviceParser> _device_parsers;
};


}


#endif
