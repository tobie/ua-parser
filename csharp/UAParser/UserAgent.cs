using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    public class UserAgent
    {
        public UserAgent() { }
        public UserAgent(string family, string major, string minor, string patch)
        {
            Family = family;
            Major = major;
            Minor = minor;
            Patch = patch;
        }
        public string Family { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Patch { get; set; }

        public override string ToString()
        {
            return string.Format("UserAgent: {0} {1}.{2} {3}", Family, Major, Minor, Patch);
        }
    }
}
