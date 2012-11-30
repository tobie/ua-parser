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

namespace ua_parser
{
    /// <summary>
    /// Collection of parsed data for a given user agent string consisting of UserAgent, OS, Device
    /// </summary>
    public class Client
    {
        public UserAgent userAgent;
        public OS os;
        public Device device;

        public Client(UserAgent userAgent, OS os, Device device)
        {
            this.userAgent = userAgent;
            this.os = os;
            this.device = device;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is Client))
                return false;

            Client o = (Client)obj;
            return ((this.userAgent != null && this.userAgent.Equals(o.userAgent)) || this.userAgent == o.userAgent) &&
                   ((this.os != null && this.os.Equals(o.os)) || this.os == o.os) &&
                   ((this.device != null && this.device.Equals(o.device)) || this.device == o.device);
        }

        public override int GetHashCode()
        {

            int h = userAgent == null ? 0 : userAgent.GetHashCode();
            h += os == null ? 0 : os.GetHashCode();
            h += device == null ? 0 : device.GetHashCode();
            return h;
        }

        public override string ToString()
        {
            return String.Format("{user_agent: {0}, os: {1}, device: {2}}", userAgent, os, device);
        }
    }
}
