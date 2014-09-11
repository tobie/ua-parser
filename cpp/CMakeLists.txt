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

cmake_minimum_required(VERSION 2.8)

set(CMAKE_CXX_FLAGS "-g")
 
if(COMMAND cmake_policy)
  cmake_policy(SET CMP0003 NEW)
endif(COMMAND cmake_policy)

add_library(ua_parser ua_parser.cpp)
target_link_libraries(ua_parser yaml-cpp boost_regex)

add_executable(ua_parser_cli main.cpp)
target_link_libraries(ua_parser_cli ua_parser)

add_executable(ua_parser_test ua_parser_test.cpp)
target_link_libraries(ua_parser_test ua_parser gtest pthread)
