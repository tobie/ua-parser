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
using YamlDotNet.RepresentationModel;

namespace ua_parser
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }

    /// <summary>
    /// YamlDotNet Extensions
    /// </summary>
    public static class YamlExtensions
    {
        public static List<Dictionary<String, String>> ToListOfDictionaries(this YamlSequenceNode yaml)
        {
            List<Dictionary<String, String>> list = new List<Dictionary<string, string>>();

            foreach (YamlMappingNode item in yaml)
            {
                Dictionary<String, String> dictionary = new Dictionary<string, string>();
                foreach (var key in item.Children.Keys)
                {
                    dictionary.Add(key.ToString(), item.Children[new YamlScalarNode(key.ToString())].ToString());
                }
                list.Add(dictionary);
            }

            return list;
        }
    }
}