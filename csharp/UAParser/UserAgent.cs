using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
  /// <summary>
  /// The UserAgent extracted from the user agent string
  /// </summary>
  public class UserAgent
  {
    internal UserAgent() { }
    internal UserAgent(string family, string major, string minor, string patch)
    {
      Family = family;
      Major = major;
      Minor = minor;
      Patch = patch;
    }
    /// <summary>
    /// The family of the user agent
    /// </summary>
    public string Family { get; set; }
    /// <summary>
    /// The major version of the user agent
    /// </summary>
    public string Major { get; set; }
    /// <summary>
    /// The minor version of the user agent
    /// </summary>
    public string Minor { get; set; }
    /// <summary>
    /// The patch version of the user agent
    /// </summary>
    public string Patch { get; set; }

    /// <summary>
    /// A string representation of the user agent
    /// </summary>
    /// <returns>string representation of the user agent</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(Family);
      string version = ToVersionString();

      if (!string.IsNullOrEmpty(version))
      {
        sb.Append(' ').Append(version);
      }

      return sb.ToString();
    }
    /// <summary>
    /// The version of the user agent as a single string
    /// </summary>
    /// <returns>the version as a string</returns>
    public string ToVersionString()
    {
      IEnumerable<string> versions = new string[] { Major, Minor, Patch }.Where(x => !string.IsNullOrEmpty(x));
      return string.Join(".", versions.ToArray());
    }
  }
}
