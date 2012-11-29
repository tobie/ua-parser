using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
    public class Device
    {
        public Device(string family, bool isMobile, bool isSpider)
        {
            Family = family;
            IsMobile = isMobile;
            IsSpider = isSpider;
        }
        public string Family { get; set; }
        public bool IsMobile { get; set; }
        public bool IsSpider { get; set; }

        public override string ToString()
        {
            return string.Format("Device: {0} {1} {2}", Family, IsMobile?"mobile":"", IsSpider?"spider":"");
        }
    }
}
