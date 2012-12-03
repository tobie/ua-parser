using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UAParser
{
    internal class UserAgentPattern
    {
        private readonly Regex m_regexPattern;
        private readonly string m_familyReplacement;
        private readonly string m_majorReplacement;
        private readonly string m_minorReplacement;

        internal UserAgentPattern(Regex regEx, string familyReplacement, string majorReplacement, string minorReplacement)
        {
            m_regexPattern = regEx;
            m_familyReplacement = familyReplacement;
            m_majorReplacement = majorReplacement;
            m_minorReplacement = minorReplacement;
        }

        public UserAgent GetMatch(string agentstring)
        {
            string family = null, v1 = null, v2 = null, v3 = null;
            var match = m_regexPattern.Match(agentstring);

            if (match.Length == 0)
            {
                return null;
            }

            int groupCount = match.Groups.Count;

            if (m_familyReplacement != null)
            {
                if (m_familyReplacement.Contains("$1") && groupCount >= 1 && match.Groups[1] != null)
                {
                    family = m_familyReplacement.ReplaceFirstOccurence("$1", Regex.Escape(match.Groups[1].Value));
                }
                else
                {
                    family = m_familyReplacement;
                }
            }
            else if (groupCount >= 1)
            {
                family = match.Groups[1].Value;
            }

            if (m_majorReplacement != null)
            {
                v1 = m_majorReplacement;
            }
            else if (groupCount >= 2)
            {
                v1 = match.Groups[2].Value;
            }

            if (m_minorReplacement != null)
            {
                v2 = m_minorReplacement;
            }
            else if (groupCount >= 3)
            {
                v2 = match.Groups[3].Value;
                if (groupCount >= 4)
                {
                    v3 = match.Groups[4].Value;
                }
            }
            return family == null ? null : new UserAgent(family, v1, v2, v3);
        }
    }

}
