/**
 * Copyright 2012 viagogo AG
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace ua_parser
{
    /// <summary>
    /// Device parsed data class
    /// </summary>
    public class Device
    {
        public String family;
        public bool isMobile, isSpider, isComputer;

        public Device(String family, bool isMobile, bool isSpider)
        {
            this.family = family;
            this.isMobile = isMobile;
            this.isSpider = isSpider;
			this.isComputer = !this.isSpider && ! this.isMobile;
        }

        public static Device FromMap(IDictionary<String, Object> m)
        {
            return new Device((String)m["family"], (Boolean)m["is_mobile"], (Boolean)m["is_spider"]);
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (!(obj is Device)) return false;

            Device o = (Device)obj;
            return ((this.family != null && this.family.Equals(o.family)) || this.family == o.family) &&
                   this.isMobile == o.isMobile &&
                   this.isSpider == o.isSpider;
        }

        public override int GetHashCode()
        {
            int h = family == null ? 0 : family.GetHashCode();
            h += isMobile ? 1429 : 2713;
            h += isSpider ? 1187 : 953;
            return h;
        }

        public override string ToString()
        {
            return String.Format("{family: {0}, is_mobile: {1}, is_spider: {2}}", family == null ? null : '"' + family + '"', isMobile, isSpider);
        }
    }
}
