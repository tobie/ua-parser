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
  public final String brand;
  public final String model;

  public Device(String family, String brand, String model) {
    this.family = family;
    this.brand  = brand;
    this.model  = model;
  }

  public static Device fromMap(Map<String, String> m) {
    return new Device(m.get("family"), m.get("brand"), m.get("model"));
  }

  @Override
  public boolean equals(Object other) {
    if (other == this) return true;
    if (!(other instanceof Device)) return false;

    Device o = (Device) other;
    return (((this.family != null && this.family.equals(o.family)) || this.family == o.family) &&
            ((this.brand  != null && this.brand.equals(o.brand))   || this.brand  == o.brand ) &&
            ((this.model  != null && this.model.equals(o.model))   || this.model  == o.model ));
  }

  @Override
  public int hashCode() {
    int h = family == null ? 0 : family.hashCode();
    h += brand == null ? 0 : brand.hashCode();
    h += model == null ? 0 : model.hashCode();
    return h;
  }

  @Override
  public String toString() {
    return String.format("{family: %s, brand: %s, model: %s}",
                         family == null ? null : '"' + family + '"',
                         brand  == null ? null : '"' + brand  + '"',
                         model  == null ? null : '"' + model  + '"');
  }
}