using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace UAParser
{
    internal static class InternalExtensions
    {
        internal static string ReplaceFirstOccurence(this string inputstring, string searchText, string replacementText)
        {
            int index = inputstring.IndexOf(searchText);
            if (index == -1)
                return inputstring;

            return inputstring.Substring(0, index) + replacementText + inputstring.Substring(index + searchText.Length);
        }

        internal static List<Dictionary<string, string>> ConvertToDictionaryList(this YamlSequenceNode yamlNode)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var item in yamlNode.OfType<YamlMappingNode>())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                list.Add(dic);
                foreach (var key in item.Children.Keys)
                {
                    dic[key.ToString()] = item.Children[key].ToString();
                }
            }
            return list;
        }

        internal static void ThrowIfNull(this object obj, string exceptionMessage)
        {
            if (obj == null)
                throw new ArgumentNullException(exceptionMessage);
        }
    }
}
