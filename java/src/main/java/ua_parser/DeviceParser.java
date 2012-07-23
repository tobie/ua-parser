package ua_parser;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Device parser using ua-parser regexes. Extracts device information from user agent strings.
 *
 * @author Steve Jiang (@sjiang) <gh at iamsteve com>
 */
public class DeviceParser {
  List<DevicePattern> patterns;
  private final Set<String> mobileUAFamilies, mobileOSFamilies;
  private final UserAgentParser uaParser;

  public DeviceParser(List<DevicePattern> patterns, UserAgentParser uaParser,
                      Set<String> mobileUAFamilies, Set<String> mobileOSFamilies) {
    this.patterns = patterns;
    this.uaParser = uaParser;
    this.mobileUAFamilies = mobileUAFamilies;
    this.mobileOSFamilies = mobileOSFamilies;
  }

  public Device parse(String agentString) {
    String device = null;
    for (DevicePattern p : patterns) {
      if ((device = p.match(agentString)) != null) {
        break;
      }
    }
    String osFamily = device == null ? "Other" : device;
    String uaFamily = uaParser.parse(agentString).family;

    return new Device(device,
                      mobileUAFamilies.contains(uaFamily) || mobileOSFamilies.contains(osFamily),
                      (device != null && device.equals("Spider")));
  }

  public static DeviceParser fromList(List<Map> configList, UserAgentParser uaParser,
                                      Set<String> mobileUAFamilies, Set<String> mobileOSFamilies) {
    List<DevicePattern> configPatterns = new ArrayList<DevicePattern>();
    for (Map<String,String> configMap : configList) {
      configPatterns.add(DeviceParser.patternFromMap(configMap));
    }
    return new DeviceParser(configPatterns, uaParser, mobileUAFamilies, mobileOSFamilies);
  }

  protected static DevicePattern patternFromMap(Map<String, String> configMap) {
    String regex = configMap.get("regex");
    if (regex == null) {
      throw new IllegalArgumentException("Device is missing regex");
    }
    return new DevicePattern(Pattern.compile(regex),
                             configMap.get("device_replacement"));
  }

  protected static class DevicePattern {
    private final Pattern pattern;
    private final String familyReplacement;

    public DevicePattern(Pattern pattern, String familyReplacement) {
      this.pattern = pattern;
      this.familyReplacement = familyReplacement;
    }

    public String match(String agentString) {
      Matcher matcher = pattern.matcher(agentString);

      if (!matcher.find()) {
        return null;
      }

      String family = null;
      if (familyReplacement != null) {
        if (familyReplacement.contains("$1") && matcher.groupCount() >= 1 && matcher.group(1) != null) {
          family = familyReplacement.replaceFirst("\\$1", matcher.group(1));
        } else {
          family = familyReplacement;
        }
      } else if (matcher.groupCount() >= 1) {
        family = matcher.group(1);
      }
      return family;
    }
  }

}