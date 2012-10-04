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
    /// User Agent parsed data class
    /// </summary>
    public class UserAgent
    {
        public String family, major, minor, patch;

        public UserAgent(String family, String major, String minor, String patch)
        {
            this.family = family;
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }

        public static UserAgent FromMap(IDictionary<String, String> m)
        {
            return new UserAgent(m["family"], m["major"], m["minor"], m["patch"]);
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (!(obj is UserAgent)) return false;

            UserAgent o = (UserAgent)obj;
            return ((this.family != null && this.family.Equals(o.family)) || this.family == o.family) &&
                   ((this.major != null && this.major.Equals(o.major)) || this.major == o.major) &&
                   ((this.minor != null && this.minor.Equals(o.minor)) || this.minor == o.minor) &&
                   ((this.patch != null && this.patch.Equals(o.patch)) || this.patch == o.patch);
        }

        public override int GetHashCode()
        {
            int h = family == null ? 0 : family.GetHashCode();
            h += major == null ? 0 : major.GetHashCode();
            h += minor == null ? 0 : minor.GetHashCode();
            h += patch == null ? 0 : patch.GetHashCode();
            return h;
        }

        public override string ToString()
        {
            return String.Format("{family: {0}, major: {1}, minor: {2}, patch: {3}}",
                                 family == null ? null : '"' + family + '"',
                                 major == null ? null : '"' + major + '"',
                                 minor == null ? null : '"' + minor + '"',
                                 patch == null ? null : '"' + patch + '"');
        }

    }
}
