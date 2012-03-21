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

import os
import re
import yaml


ROOT_DIR = os.path.abspath(os.path.dirname(__file__))

class UserAgentParser(object):
  def __init__(self, pattern, family_replacement=None, v1_replacement=None):
    """Initialize UserAgentParser.

    Args:
      pattern: a regular expression string
      family_replacement: a string to override the matched family (optional)
      v1_replacement: a string to override the matched v1 (optional)
    """
    self.pattern = pattern
    self.user_agent_re = re.compile(self.pattern)
    self.family_replacement = family_replacement
    self.v1_replacement = v1_replacement

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
      elif match.lastindex >= 2:
        v1 = match.group(2)

      if match.lastindex >= 3:
        v2 = match.group(3)
        if match.lastindex >= 4:
          v3 = match.group(4)
    return family, v1, v2, v3

class OSParser(object):
  def __init__(self, pattern, os_replacement=None ):
    """Initialize UserAgentParser.

    Args:
      pattern: a regular expression string
      os_replacement: a string to override the matched os (optional)
    """
    self.pattern = pattern
    self.user_agent_re = re.compile(self.pattern)
    self.os_replacement = os_replacement

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

      if match.lastindex >= 2:
        os_v1 = match.group(2)
        if match.lastindex >= 3:
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


def ParseAll(user_agent_string, **jsParseBits):
  """ Parse all the things
      Args:
          user_agent_string: the full user agent string
          jsParseBits: javascript override bits
      Returns:
          flat dictionary containing all parsed bits
  """
  jsParseBits = jsParseBits or {}

  resultUA = ParseUserAgent(user_agent_string, **jsParseBits)
  resultOS = ParseOS(user_agent_string, **jsParseBits)

  resultAll = {}
  for result in (resultUA, resultOS):
    resultAll.update(result)

  return resultAll

def ParseUserAgent(user_agent_string, **jsParseBits):
  """ Parses the user-agent string for user agent (browser) info.
      Args:
        user_agent_string: The full user-agent string.
        jsParseBits: javascript override bits
      Returns:
        flat dictionary containing parsed bits
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
      v1 = jsOverride['v1']
      v2 = jsOverride['v2']
      v3 = jsOverride['v3']

  family = family or 'Other'
  return {'family': family, 'v1': v1,  'v2': v2, 'v3': v3}

def ParseOS(user_agent_string, **jsParseBits):
  """ Parses the user-agent string for operating system info
      Args:
        user_agent_string: The full user-agent string.
        jsParseBits: javascript override bits
      Returns:
        flat dictionary containing parsed bits
  """
  for osParser in OS_PARSERS:
    os, os_v1, os_v2, os_v3, os_v4 = osParser.Parse(user_agent_string)
    if os:
      break
  os = os or 'Other'
  return { 'os': os, 'os_v1': os_v1, 'os_v2': os_v2, 'os_v3': os_v3, 'os_v4': os_v4 }

def ParseDevice(user_agent_string, **jsParseBits):
  """ incomplete! """
  for deviceParser in DEVICE_PARSERS:
    device = deviceParser.Parse(user_agent_string)
    if device:
      break

  device = device or 'Other'
  return {'device': device}

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

def Parse(user_agent_string, js_user_agent_string=None,
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
    cf_family, v1, v2, v3 = Parse(js_user_agent_string)

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
yamlFile = open(os.path.join(ROOT_DIR, '../regexes.yaml'))
yaml = yaml.load(yamlFile)
yamlFile.close()

USER_AGENT_PARSERS = []
for parser in yaml['user_agent_parsers']:
  regex = parser['regex']

  family_replacement = None
  if 'family_replacement' in parser:
    family_replacement = parser['family_replacement']

  v1_replacement = None
  if 'v1_replacement' in parser:
    v1_replacement = parser['v1_replacement']

  USER_AGENT_PARSERS.append(UserAgentParser(regex, family_replacement, v1_replacement))

OS_PARSERS = []
for parser in yaml['os_parsers']:
  regex = parser['regex']

  os_replacement = None
  if 'os_replacement' in parser:
    os_replacement = parser['os_replacement']

  OS_PARSERS.append(OSParser(regex, os_replacement))

DEVICE_PARSERS = []
for parser in yaml['device_parsers']:
  regex = parser['regex']

  device_replacement = None
  if 'device_replacement' in parser:
    device_replacement = parser['device_replacement']

DEVICE_PARSERS.append(DeviceParser(regex, device_replacement))
