using System;
using System.Collections.Generic;
using System.Linq;

namespace UAParser
{
  /// <summary>
  /// The device information extracted from the user agent string
  /// </summary>
  public class Device
  {
    internal Device(string family, bool isMobile, bool isSpider)
    {
      Family = family;
      IsMobile = isMobile;
      IsSpider = isSpider;
    }
    /// <summary>
    /// The family element of the device
    /// </summary>
    public string Family { get; set; }
    /// <summary>
    /// True if the device is considered a mobile device
    /// </summary>
    public bool IsMobile { get; set; }
    /// <summary>
    /// True if the device is considered a spider or web crawler
    /// </summary>
    public bool IsSpider { get; set; }

    /// <summary>
    /// A string representation of the device
    /// </summary>
    /// <returns>string representation for the device</returns>
    public override string ToString()
    {
      return Family;
    }
  }
}
