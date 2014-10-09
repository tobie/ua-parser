using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace UAParser.Tests
{
    internal static class InternalExtensions
    {
        internal static List<Dictionary<string, string>> ConvertToDictionaryList(this YamlSequenceNode yamlNode)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var item in yamlNode.OfType<YamlMappingNode>())
            {
                list.Add(ConvertToDictionary(item));
            }
            return list;
        }
        internal static Dictionary<string, string> ConvertToDictionary(this YamlMappingNode yamlNode)
        {
          Dictionary<string, string> dic = new Dictionary<string, string>();
          foreach (var key in yamlNode.Children.Keys)
          {
            dic[key.ToString()] = yamlNode.Children[key].ToString();
          }
          return dic;
        }
        internal static string GetTestResources(this object self, string name)
        {
            using (Stream s = typeof(TestResourceTests).Assembly.GetManifestResourceStream(name))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not locate an embedded test resource with name: " + name);
                using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
