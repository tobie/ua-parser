using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
  /// <summary>
  /// Represents OS information extracted from the user agent string
  /// </summary>
  public class OS
  {
    internal OS() { }
    internal OS(string family, string major, string minor, string patch, string patchMinor)
    {
      Family = family;
      Major = major;
      Minor = minor;
      Patch = patch;
      PatchMinor = patchMinor;
    }
    /// <summary>
    /// The OS family
    /// </summary>
    public string Family { get; set; }
    /// <summary>
    /// The Major version of the OS
    /// </summary>
    public string Major { get; set; }
    /// <summary>
    /// The Minor version of the OS
    /// </summary>
    public string Minor { get; set; }
    /// <summary>
    /// The patch version of the OS, if applicable
    /// </summary>
    public string Patch { get; set; }
    /// <summary>
    /// The minor patch version of the OS, if applicable
    /// </summary>
    public string PatchMinor { get; set; }

    /// <summary>
    /// Returns the string representation for the OS
    /// </summary>
    /// <returns>The OS instance as a string</returns>
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
    /// The version of the OS as a single string
    /// </summary>
    /// <returns></returns>
    public string ToVersionString()
    {
      IEnumerable<string> versions = new string[] { Major, Minor, Patch, PatchMinor }.Where(x => !string.IsNullOrEmpty(x));
      return string.Join(".", versions.ToArray());
    }
  }
}
