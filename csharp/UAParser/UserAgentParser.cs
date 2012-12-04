using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    internal class UserAgentParser
    {
        private readonly List<UserAgentPattern> m_patterns;
        internal UserAgentParser(List<UserAgentPattern> patterns)
        {
            m_patterns = patterns;
        }

        public UserAgent ParseAgentString(string agentstring)
        {
            UserAgent agent;
            foreach (UserAgentPattern p in m_patterns)
            {
                if ((agent = p.GetMatch(agentstring)) != null)
                {
                    return agent;
                }
            }
            return new UserAgent("Other", null, null, null);
        }
    }
}
