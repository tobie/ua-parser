using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UAParser
{
    internal class DevicePattern
    {
        private readonly Regex m_pattern;
        private readonly string m_familyReplacement;

        public DevicePattern(Regex pattern, string familyReplacement)
        {
            m_pattern = pattern;
            m_familyReplacement = familyReplacement;
        }

        public string GetMatch(string agentString)
        {
            var match = m_pattern.Match(agentString);

            if (match.Length == 0)
            {
                return null;
            }

            string family = null;

            if (m_familyReplacement != null)
            {
                if (m_familyReplacement.Contains("$1") && match.Groups.Count >= 1 && match.Groups[1] != null)
                {
                    family = m_familyReplacement.ReplaceFirstOccurence("$1", Regex.Escape(match.Groups[1].Value));
                }
                else
                {
                    family = m_familyReplacement;
                }
            }
            else if (match.Groups.Count >= 1)
            {
                family = match.Groups[1].Value;
            }
            return family;
        }
    }

}
