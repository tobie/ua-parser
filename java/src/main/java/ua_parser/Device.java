/**
 * Copyright 2012 Twitter, Inc
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

package ua_parser;

import java.util.Map;

/**
 * Device parsed data class
 *
 * @author Steve Jiang (@sjiang) <gh at iamsteve com>
 */
public class Device {
  public final String family;
  public final boolean isMobile, isSpider;

  public Device(String family, boolean isMobile, boolean isSpider) {
    this.family = family;
    this.isMobile = isMobile;
    this.isSpider = isSpider;
  }

  public static Device fromMap(Map<String, Object> m) {
    return new Device((String) m.get("family"), (Boolean) m.get("is_mobile"),
                      (Boolean) m.get("is_spider"));
  }

  @Override
  public boolean equals(Object other) {
    if (other == this) return true;
    if (!(other instanceof Device)) return false;

    Device o = (Device) other;
    return ((this.family != null && this.family.equals(o.family)) || this.family == o.family) &&
           this.isMobile == o.isMobile &&
           this.isSpider == o.isSpider;
  }

  @Override
  public int hashCode() {
    int h = family == null ? 0 : family.hashCode();
    h += isMobile ? 1429 : 2713;
    h += isSpider ? 1187 : 953;
    return h;
  }

  @Override
  public String toString() {
    return String.format("{family: %s, is_mobile: %s, is_spider: %s}",
                         family == null ? null : '"' + family + '"',
                         isMobile, isSpider);
  }
}