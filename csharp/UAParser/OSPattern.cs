using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UAParser
{
    internal class OSPattern
    {
        private readonly Regex m_pattern;
        private readonly string m_osReplacement;
        private readonly string m_v1Replacement;
        private readonly string m_v2Replacement;

        public OSPattern(Regex pattern, string osReplacement, string v1Replacement, string v2Replacement)
        {
            m_pattern = pattern;
            m_osReplacement = osReplacement;
            m_v1Replacement = v1Replacement;
            m_v2Replacement = v2Replacement;
        }

        public OS GetMatch(string agentString)
        {
            string family = null, v1 = null, v2 = null, v3 = null, v4 = null;
            var match = m_pattern.Match(agentString);

            if (match.Length == 0)
            {
                return null;
            }

            int groupCount = match.Groups.Count;

            if (m_osReplacement != null)
            {
                family = m_osReplacement;
            }
            else if (groupCount >= 1)
            {
                family = match.Groups[1].Value;
            }

            if (m_v1Replacement != null)
            {
                v1 = m_v1Replacement;
            }
            else if (groupCount >= 2)
            {
                v1 = match.Groups[2].Value;
                if (m_v2Replacement != null)
                {
                    v2 = m_v2Replacement;
                }
                else if (groupCount >= 3)
                {
                    v2 = match.Groups[3].Value;
                    if (groupCount >= 4)
                    {
                        v3 = match.Groups[4].Value;
                        if (groupCount >= 5)
                        {
                            v4 = match.Groups[5].Value;
                        }
                    }
                }
            }
            return family == null ? null : new OS(family, v1, v2, v3, v4);
        }
    }

}
