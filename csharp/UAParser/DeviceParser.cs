using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    internal class DeviceParser
    {
       private readonly List<DevicePattern> m_patterns;
        private readonly List<string> m_mobileUAFamilies;
        private readonly List<string> m_mobileOSFamilies;
        private readonly UserAgentParser m_userAgentParser;

        public DeviceParser(List<DevicePattern> patterns, UserAgentParser uaParser,
                            List<string> mobileUAFamilies, List<string> mobileOSFamilies)
        {
            m_patterns = patterns;
            m_userAgentParser = uaParser;
            m_mobileUAFamilies = mobileUAFamilies;
            m_mobileOSFamilies = mobileOSFamilies;
        }

        public Device ParseUserAgentString(string agentString)
        {
            return ParseUserAgentString(agentString, m_userAgentParser.ParseAgentString(agentString).Family);
        }

        public Device ParseUserAgentString(string agentString, string userAgentFamily)
        {
            string device = null;
            foreach (DevicePattern p in m_patterns)
            {
                if ((device = p.GetMatch(agentString)) != null)
                {
                    break;
                }
            }

            string osFamily = device == null ? "Other" : device;
            userAgentFamily = userAgentFamily == null ? "Other" : userAgentFamily;

            return new Device(device,
                              m_mobileUAFamilies.Contains(userAgentFamily) || m_mobileOSFamilies.Contains(osFamily),
                              (device != null && device.Equals("Spider")));
        }
    }
}
