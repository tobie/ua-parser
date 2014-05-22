using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace UAParser
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
    }
}
