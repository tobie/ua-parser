/**
 * Copyright 2012 Twitter, Inc
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package ua_parser;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Device parser using ua-parser regexes. Extracts device information from user agent strings.
 *
 * @author Steve Jiang (@sjiang) <gh at iamsteve com>
 */
public class DeviceParser {
  List<DevicePattern> patterns;

  public DeviceParser(List<DevicePattern> patterns) {
    this.patterns = patterns;
  }

  public Device parse(String agentString) {
    if (agentString == null) {
      return null;
    }

    Device device = null;
    for (DevicePattern p : patterns) {
      if ((device = p.match(agentString)) != null) {
        break;
      }
    }
    if (device != null) {
      return device;
    }

    return new Device("Other", null, null);
  }

  public static DeviceParser fromList(List<Map<String,String>> configList) {
    List<DevicePattern> configPatterns = new ArrayList<DevicePattern>();
    for (Map<String,String> configMap : configList) {
      configPatterns.add(DeviceParser.patternFromMap(configMap));
    }
    return new DeviceParser(configPatterns);
  }

  protected static DevicePattern patternFromMap(Map<String, String> configMap) {
    String regex = configMap.get("regex");
    String regex_flag = configMap.get("regex_flag");
    Pattern pattern;
    if (regex == null) {
      throw new IllegalArgumentException("Device is missing regex");
    }
    if (null != regex_flag && regex_flag.equals("i")) {
      pattern = Pattern.compile(regex, Pattern.CASE_INSENSITIVE);
    }
    else {
      pattern = Pattern.compile(regex);
    }
    
    return new DevicePattern(pattern,
                             configMap.get("device_replacement"),
                             configMap.get("brand_replacement"),
                             configMap.get("model_replacement"));
  }

  protected static class DevicePattern {
    private final Pattern pattern;
    private final String familyReplacement;
    private final String brandReplacement;
    private final String modelReplacement;
    private final Pattern patternCut = Pattern.compile("(?=\\$([0-9]))");
    private final Pattern patternNum = Pattern.compile(".*\\$([0-9]).*");

    public DevicePattern(Pattern pattern,
                         String familyReplacement,
                         String brandReplacement,
                         String modelReplacement) {
      this.pattern = pattern;
      this.familyReplacement = familyReplacement;
      this.brandReplacement  = brandReplacement;
      this.modelReplacement  = modelReplacement;
    }

    public Device match(String agentString) {
      String family = null, brand = null, model = null;
      Matcher matcher = pattern.matcher(agentString);

      if (!matcher.find()) {
        return null;
      }
      
      family = multiReplace(familyReplacement, matcher);
      family = "".equals(family) ? null : family;
      if (familyReplacement == null && matcher.groupCount() > 0 && matcher.group(1) != null){
        family = matcher.group(1);
      }

      brand = multiReplace(brandReplacement, matcher);
      brand = "".equals(brand) ? null : brand;

      model = multiReplace(modelReplacement, matcher);
      model = "".equals(model) ? null : model;
      if (modelReplacement == null && matcher.groupCount() > 0 && matcher.group(1) != null){
        model = matcher.group(1);
      }

      //System.out.println("MATCHED:\"" + agentString + "\" WITH " + pattern + " RESULTING IN" + new Device(family, brand, model));
      return new Device(family, brand, model);
    }

    private String multiReplace(String input, Matcher matcher){
      String output = "";
      
      if (null != input) {
        String[] cut = patternCut.split(input);
        for (int i=0; i<cut.length; i++) {
          Matcher m = patternNum.matcher(cut[i]);
          if (m.find()) {
            int markerPos = Integer.parseInt(m.group(1));
            if (matcher.find(0) && matcher.groupCount() >= markerPos && matcher.group(markerPos) != null) {
              cut[i] = cut[i].replaceFirst("\\$"+markerPos, Matcher.quoteReplacement(matcher.group(markerPos)));
            }
            else {
              cut[i] = cut[i].replaceFirst("\\$"+markerPos, "");
            }
          }
          output += cut[i];
        }
      }
      return output.trim();
    }
  }
}
