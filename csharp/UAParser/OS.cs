using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    public class OS
    {
        public OS() { }
        public OS(string family, string major, string minor, string patch, string patchMinor)
        {
            Family = family;
            Major = major;
            Minor = minor;
            Patch = patch;
            PatchMinor = patchMinor;
        }
        public string Family { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Patch { get; set; }
        public string PatchMinor { get; set; }

        public override string ToString()
        {
            return string.Format("OS: {0} {1}.{2} {3} {4}", Family, Major, Minor, Patch, PatchMinor);
        }

        public string ToVersionString()
        {
          IEnumerable<string> versions = new string[] { Major, Minor, Patch, PatchMinor }.Where(x => !string.IsNullOrEmpty(x));
          return string.Join(".", versions.ToArray());
        }
    }
}
