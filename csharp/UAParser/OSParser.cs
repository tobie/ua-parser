using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    internal class OSParser
    {
        private readonly List<OSPattern> m_patterns;
        internal OSParser(List<OSPattern> patterns)
        {
            m_patterns = patterns;
        }
        public OS ParseUserAgentString(string agentString)
        {
            OS os;
            foreach (OSPattern p in m_patterns)
            {
                if ((os = p.GetMatch(agentString)) != null)
                {
                    return os;
                }
            }
            return new OS("Other", null, null, null, null);
        }
    }
}
