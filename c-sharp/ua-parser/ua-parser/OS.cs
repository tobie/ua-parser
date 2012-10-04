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
    /// Operating System parsed data class
    /// </summary>
    public class OS
    {
        public String family, major, minor, patch, patchMinor;

        public OS(String family, String major, String minor, String patch, String patchMinor)
        {
            this.family = family;
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.patchMinor = patchMinor;
        }

        public static OS fromMap(IDictionary<String, String> m)
        {
            return new OS(m["family"], m["major"], m["minor"], m["patch"], m["patch_minor"]);
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (!(obj is OS)) return false;

            OS o = (OS)obj;
            return ((this.family != null && this.family.Equals(o.family)) || this.family == o.family) &&
                   ((this.major != null && this.major.Equals(o.major)) || this.major == o.major) &&
                   ((this.minor != null && this.minor.Equals(o.minor)) || this.minor == o.minor) &&
                   ((this.patch != null && this.patch.Equals(o.patch)) || this.patch == o.patch) &&
                   ((this.patchMinor != null && this.patchMinor.Equals(o.patchMinor)) || this.patchMinor == o.patchMinor);
        }

        public override int GetHashCode()
        {
            int h = family == null ? 0 : family.GetHashCode();
            h += major == null ? 0 : major.GetHashCode();
            h += minor == null ? 0 : minor.GetHashCode();
            h += patch == null ? 0 : patch.GetHashCode();
            h += patchMinor == null ? 0 : patchMinor.GetHashCode();
            return h;
        }

        public override string ToString()
        {
            return String.Format("{family: {0}, major: {1}, minor: {2}, patch: {3}, patch_minor: {4}}",
                                 family == null ? null : '"' + family + '"',
                                 major == null ? null : '"' + major + '"',
                                 minor == null ? null : '"' + minor + '"',
                                 patch == null ? null : '"' + patch + '"',
                                 patchMinor == null ? null : '"' + patchMinor + '"');
        }
    }
}
