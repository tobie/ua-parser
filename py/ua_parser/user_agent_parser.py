#!/usr/bin/env python
#
# Copyright 2009 Google Inc.
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

"""Python implementation of the UA parser."""

__author__ = 'Lindsey Simon <elsigh@gmail.com>'

import json
import os
import re

# pip may copy regexes.yaml to different places depending on the OS.
# For example, on Mac pip copies regexes.yaml to the folder where
# user_agent_parser.py lives where as Fedora leaves regexes.yaml to "data" dir
# See https://github.com/tobie/ua-parser/issues/209 for the complete discussion

ROOT_DIR = os.path.abspath(os.path.dirname(__file__))
DATA_DIR = os.path.abspath(os.path.join(ROOT_DIR, '..', 'data'))
regex_dir = ROOT_DIR if os.path.exists(os.path.join(ROOT_DIR, 'regexes.yaml')) else DATA_DIR

from pkg_resources import resource_filename


class UserAgentParser(object):
    def __init__(self, pattern, family_replacement=None, v1_replacement=None, v2_replacement=None):
        """Initialize UserAgentParser.

        Args:
          pattern: a regular expression string
          family_replacement: a string to override the matched family (optional)
          v1_replacement: a string to override the matched v1 (optional)
          v2_replacement: a string to override the matched v2 (optional)
        """
        self.pattern = pattern
        self.user_agent_re = re.compile(self.pattern)
        self.family_replacement = family_replacement
        self.v1_replacement = v1_replacement
        self.v2_replacement = v2_replacement

    def MatchSpans(self, user_agent_string):
        match_spans = []
        match = self.user_agent_re.search(user_agent_string)
        if match:
            match_spans = [match.span(group_index)
                           for group_index in range(1, match.lastindex + 1)]
        return match_spans

    def Parse(self, user_agent_string):
        family, v1, v2, v3 = None, None, None, None
        match = self.user_agent_re.search(user_agent_string)
        if match:
            if self.family_replacement:
                if re.search(r'\$1', self.family_replacement):
                    family = re.sub(r'\$1', match.group(1), self.family_replacement)
                else:
                    family = self.family_replacement
            else:
                family = match.group(1)

            if self.v1_replacement:
                v1 = self.v1_replacement
            elif match.lastindex and match.lastindex >= 2:
                v1 = match.group(2)

            if self.v2_replacement:
                v2 = self.v2_replacement
            elif match.lastindex and match.lastindex >= 3:
                v2 = match.group(3)

            if match.lastindex and match.lastindex >= 4:
                v3 = match.group(4)

        return family, v1, v2, v3


class OSParser(object):
    def __init__(self, pattern, os_replacement=None, os_v1_replacement=None, os_v2_replacement=None):
        """Initialize UserAgentParser.

        Args:
          pattern: a regular expression string
          os_replacement: a string to override the matched os (optional)
          os_v1_replacement: a string to override the matched v1 (optional)
          os_v2_replacement: a string to override the matched v2 (optional)
        """
        self.pattern = pattern
        self.user_agent_re = re.compile(self.pattern)
        self.os_replacement = os_replacement
        self.os_v1_replacement = os_v1_replacement
        self.os_v2_replacement = os_v2_replacement

    def MatchSpans(self, user_agent_string):
        match_spans = []
        match = self.user_agent_re.search(user_agent_string)
        if match:
            match_spans = [match.span(group_index)
                           for group_index in range(1, match.lastindex + 1)]
        return match_spans

    def Parse(self, user_agent_string):
        os, os_v1, os_v2, os_v3, os_v4 = None, None, None, None, None
        match = self.user_agent_re.search(user_agent_string)
        if match:
            if self.os_replacement:
                os = self.os_replacement
            else:
                os = match.group(1)

            if self.os_v1_replacement:
                os_v1 = self.os_v1_replacement
            elif match.lastindex >= 2:
                os_v1 = match.group(2)

            if self.os_v2_replacement:
                os_v2 = self.os_v2_replacement
            elif match.lastindex >= 3:
                os_v2 = match.group(3)

            if match.lastindex >= 4:
                os_v3 = match.group(4)
                if match.lastindex >= 5:
                    os_v4 = match.group(5)

        return os, os_v1, os_v2, os_v3, os_v4


class DeviceParser(object):
    def __init__(self, pattern, device_replacement=None):
        """Initialize UserAgentParser.

        Args:
          pattern: a regular expression string
          device_replacement: a string to override the matched device (optional)
        """
        self.pattern = pattern
        self.user_agent_re = re.compile(self.pattern)
        self.device_replacement = device_replacement

    def MatchSpans(self, user_agent_string):
        match_spans = []
        match = self.user_agent_re.search(user_agent_string)
        if match:
            match_spans = [match.span(group_index)
                           for group_index in range(1, match.lastindex + 1)]
        return match_spans

    def Parse(self, user_agent_string):
        device = None
        match = self.user_agent_re.search(user_agent_string)
        if match:
            if self.device_replacement:
                if re.search(r'\$1', self.device_replacement):
                    device = re.sub(r'\$1', match.group(1), self.device_replacement)
                else:
                    device = self.device_replacement
            else:
                device = match.group(1)

        return device


def Parse(user_agent_string, **jsParseBits):
    """ Parse all the things
    Args:
      user_agent_string: the full user agent string
      jsParseBits: javascript override bits
    Returns:
      A dictionary containing all parsed bits
    """
    jsParseBits = jsParseBits or {}
    return {
        'user_agent': ParseUserAgent(user_agent_string, **jsParseBits),
        'os': ParseOS(user_agent_string, **jsParseBits),
        'device': ParseDevice(user_agent_string, **jsParseBits),
        'string': user_agent_string
    }


def ParseUserAgent(user_agent_string, **jsParseBits):
    """ Parses the user-agent string for user agent (browser) info.
    Args:
      user_agent_string: The full user-agent string.
      jsParseBits: javascript override bits.
    Returns:
      A dictionary containing parsed bits.
    """
    if 'js_user_agent_family' in jsParseBits and jsParseBits['js_user_agent_family'] != '':
        family = jsParseBits['js_user_agent_family']
        if 'js_user_agent_v1' in jsParseBits:
            v1 = jsParseBits['js_user_agent_v1'] or None
        if 'js_user_agent_v2' in jsParseBits:
            v2 = jsParseBits['js_user_agent_v2'] or None
        if 'js_user_agent_v3' in jsParseBits:
            v3 = jsParseBits['js_user_agent_v3'] or None
    else:
        for uaParser in USER_AGENT_PARSERS:
            family, v1, v2, v3 = uaParser.Parse(user_agent_string)
            if family:
                break

    # Override for Chrome Frame IFF Chrome is enabled.
    if 'js_user_agent_string' in jsParseBits:
        js_user_agent_string = jsParseBits['js_user_agent_string']
        if (js_user_agent_string and js_user_agent_string.find('Chrome/') > -1 and
            user_agent_string.find('chromeframe') > -1):
            jsOverride = {}
            jsOverride = ParseUserAgent(js_user_agent_string)
            family = 'Chrome Frame (%s %s)' % (family, v1)
            v1 = jsOverride['major']
            v2 = jsOverride['minor']
            v3 = jsOverride['patch']

    family = family or 'Other'
    return {
        'family': family,
        'major': v1,
        'minor': v2,
        'patch': v3
    }


def ParseOS(user_agent_string, **jsParseBits):
    """ Parses the user-agent string for operating system info
    Args:
      user_agent_string: The full user-agent string.
      jsParseBits: javascript override bits.
    Returns:
      A dictionary containing parsed bits.
    """
    for osParser in OS_PARSERS:
        os, os_v1, os_v2, os_v3, os_v4 = osParser.Parse(user_agent_string)
        if os:
            break
    os = os or 'Other'
    return {
        'family': os,
        'major': os_v1,
        'minor': os_v2,
        'patch': os_v3,
        'patch_minor': os_v4
    }


def ParseDevice(user_agent_string):
    """ Parses the user-agent string for device info.
    Args:
        user_agent_string: The full user-agent string.
        ua_family: The parsed user agent family name.
    Returns:
        A dictionary containing parsed bits.
    """
    for deviceParser in DEVICE_PARSERS:
        device = deviceParser.Parse(user_agent_string)
        if device:
            break

    if device == None:
        device = 'Other'

    return {
        'family': device
    }


def PrettyUserAgent(family, v1=None, v2=None, v3=None):
    """Pretty user agent string."""
    if v3:
        if v3[0].isdigit():
            return '%s %s.%s.%s' % (family, v1, v2, v3)
        else:
            return '%s %s.%s%s' % (family, v1, v2, v3)
    elif v2:
        return '%s %s.%s' % (family, v1, v2)
    elif v1:
        return '%s %s' % (family, v1)
    return family


def PrettyOS(os, os_v1=None, os_v2=None, os_v3=None, os_v4=None):
    """Pretty os string."""
    if os_v4:
        return '%s %s.%s.%s.%s' % (os, os_v1, os_v2, os_v3, os_v4)
    if os_v3:
        if os_v3[0].isdigit():
            return '%s %s.%s.%s' % (os, os_v1, os_v2, os_v3)
        else:
            return '%s %s.%s%s' % (os, os_v1, os_v2, os_v3)
    elif os_v2:
        return '%s %s.%s' % (os, os_v1, os_v2)
    elif os_v1:
        return '%s %s' % (os, os_v1)
    return os


def ParseWithJSOverrides(user_agent_string,
                         js_user_agent_string=None,
                         js_user_agent_family=None,
                         js_user_agent_v1=None,
                         js_user_agent_v2=None,
                         js_user_agent_v3=None):
    """ backwards compatible. use one of the other Parse methods instead! """

    # Override via JS properties.
    if js_user_agent_family is not None and js_user_agent_family != '':
        family = js_user_agent_family
        v1 = None
        v2 = None
        v3 = None
        if js_user_agent_v1 is not None:
            v1 = js_user_agent_v1
        if js_user_agent_v2 is not None:
            v2 = js_user_agent_v2
        if js_user_agent_v3 is not None:
            v3 = js_user_agent_v3
    else:
        for parser in USER_AGENT_PARSERS:
            family, v1, v2, v3 = parser.Parse(user_agent_string)
            if family:
                break

    # Override for Chrome Frame IFF Chrome is enabled.
    if (js_user_agent_string and js_user_agent_string.find('Chrome/') > -1 and
        user_agent_string.find('chromeframe') > -1):
        family = 'Chrome Frame (%s %s)' % (family, v1)
        ua_dict = ParseUserAgent(js_user_agent_string)
        v1 = ua_dict['major']
        v2 = ua_dict['minor']
        v3 = ua_dict['patch']

    return family or 'Other', v1, v2, v3


def Pretty(family, v1=None, v2=None, v3=None):
    """ backwards compatible. use PrettyUserAgent instead! """
    if v3:
        if v3[0].isdigit():
            return '%s %s.%s.%s' % (family, v1, v2, v3)
        else:
            return '%s %s.%s%s' % (family, v1, v2, v3)
    elif v2:
        return '%s %s.%s' % (family, v1, v2)
    elif v1:
        return '%s %s' % (family, v1)
    return family


def GetFilters(user_agent_string, js_user_agent_string=None,
               js_user_agent_family=None,
               js_user_agent_v1=None,
               js_user_agent_v2=None,
               js_user_agent_v3=None):
    """Return the optional arguments that should be saved and used to query.

    js_user_agent_string is always returned if it is present. We really only need
    it for Chrome Frame. However, I added it in the generally case to find other
    cases when it is different. When the recording of js_user_agent_string was
    added, we created new records for all new user agents.

    Since we only added js_document_mode for the IE 9 preview case, it did not
    cause new user agent records the way js_user_agent_string did.

    js_document_mode has since been removed in favor of individual property
    overrides.

    Args:
      user_agent_string: The full user-agent string.
      js_user_agent_string: JavaScript ua string from client-side
      js_user_agent_family: This is an override for the family name to deal
          with the fact that IE platform preview (for instance) cannot be
          distinguished by user_agent_string, but only in javascript.
      js_user_agent_v1: v1 override - see above.
      js_user_agent_v2: v1 override - see above.
      js_user_agent_v3: v1 override - see above.
    Returns:
      {js_user_agent_string: '[...]', js_family_name: '[...]', etc...}
    """
    filters = {}
    filterdict = {
      'js_user_agent_string': js_user_agent_string,
      'js_user_agent_family': js_user_agent_family,
      'js_user_agent_v1': js_user_agent_v1,
      'js_user_agent_v2': js_user_agent_v2,
      'js_user_agent_v3': js_user_agent_v3
    }
    for key, value in filterdict.items():
        if value is not None and value != '':
            filters[key] = value
    return filters


# Build the list of user agent parsers from YAML
UA_PARSER_YAML = os.getenv("UA_PARSER_YAML")
regexes = None

if not UA_PARSER_YAML:
    yamlPath = resource_filename(__name__, 'regexes.yaml')
    json_path = resource_filename(__name__, 'regexes.json')
else:
    import yaml

    yamlFile = open(UA_PARSER_YAML)
    regexes = yaml.load(yamlFile)
    yamlFile.close()

# If UA_PARSER_YAML is not specified, load regexes from regexes.json before
# falling back to yaml format
if regexes is None:
    try:
        json_file = open(json_path)
        regexes = json.loads(json_file.read())
        json_file.close()
    except IOError:
        import yaml

        yamlFile = open(yamlPath)
        regexes = yaml.load(yamlFile)
        yamlFile.close()


USER_AGENT_PARSERS = []
for _ua_parser in regexes['user_agent_parsers']:
    _regex = _ua_parser['regex']

    _family_replacement = None
    if 'family_replacement' in _ua_parser:
        _family_replacement = _ua_parser['family_replacement']

    _v1_replacement = None
    if 'v1_replacement' in _ua_parser:
        _v1_replacement = _ua_parser['v1_replacement']

    _v2_replacement = None
    if 'v2_replacement' in _ua_parser:
        _v2_replacement = _ua_parser['v2_replacement']

    USER_AGENT_PARSERS.append(UserAgentParser(_regex,
                                              _family_replacement,
                                              _v1_replacement,
                                              _v2_replacement))

OS_PARSERS = []
for _os_parser in regexes['os_parsers']:
    _regex = _os_parser['regex']

    _os_replacement = None
    if 'os_replacement' in _os_parser:
        _os_replacement = _os_parser['os_replacement']

    _os_v1_replacement = None
    if 'os_v1_replacement' in _os_parser:
        _os_v1_replacement = _os_parser['os_v1_replacement']

    _os_v2_replacement = None
    if 'os_v2_replacement' in _os_parser:
        _os_v2_replacement = _os_parser['os_v2_replacement']

    OS_PARSERS.append(OSParser(_regex,
                               _os_replacement,
                               _os_v1_replacement,
                               _os_v2_replacement))


DEVICE_PARSERS = []
for _device_parser in regexes['device_parsers']:
    _regex = _device_parser['regex']

    _device_replacement = None
    if 'device_replacement' in _device_parser:
        _device_replacement = _device_parser['device_replacement']

    DEVICE_PARSERS.append(DeviceParser(_regex, _device_replacement))

