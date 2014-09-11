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
# limitations under the License.#include "ua_parser.h"
*/

#include "ua_parser.h"

int main(int argc, char *argv[]) {
  std::string user_agent_string("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Ubuntu Chromium/30.0.1599.114 Chrome/30.0.1599.114 Safari/537.36");
  std::string yaml_file("../regexes.yaml");

  if (argc>1 && std::string(argv[1])=="--help") {
    std::cout << "Usage: ua_parser_cli [user agent string] [regexes.yaml]" << std::endl;
    return 1;
  } else if (argc>1)
    user_agent_string = argv[1];

  if (argc>2)
    yaml_file = argv[2];

  ua_parser::Parser uap(yaml_file);

  ua_parser::UserAgent ua = uap.Parse(user_agent_string);

  if (ua.browser.family.empty())
    return 2;

  std::cout << ua.browser.family << std::endl;
  std::cout << ua.os.os << std::endl;
  std::cout << ua.device << std::endl;
}
