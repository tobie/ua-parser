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

/**
 * @author Steve Jiang (@sjiang) <gh at iamsteve com>
 */
public class OSTest extends DataTest<OS> {
  @Override
  protected OS getRandomInstance(long seed, StringGenerator g) {
    random.setSeed(seed);
    String family = g.getString(256),
           major = (random.nextBoolean() ? g.getString(8): null),
           minor = (random.nextBoolean() ? g.getString(8): null),
           patch = (random.nextBoolean() ? g.getString(8): null),
           patchMinor = (random.nextBoolean() ? g.getString(8): null);
    return new OS(family, major, minor, patch, patchMinor);
  }

  @Override
  protected OS getBlankInstance() {
    return new OS(null, null, null, null, null);
  }
}