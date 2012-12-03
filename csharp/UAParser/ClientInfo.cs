using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAParser
{
  /// <summary>
  /// Represents the various parts of information extracted from a user agent string
  /// </summary>
  public class ClientInfo
  {
    /// <summary>
    /// The operating system information
    /// </summary>
    public OS OS { get; private set; }
    /// <summary>
    /// The device information
    /// </summary>
    public Device Device { get; private set; }
    /// <summary>
    /// The user agent information
    /// </summary>
    public UserAgent UserAgent { get; private set; }

    /// <summary>
    /// Constructing the client information
    /// </summary>
    /// <param name="os">the operating system information</param>
    /// <param name="device">the device information</param>
    /// <param name="userAgent">the user agent information</param>
    public ClientInfo(OS os, Device device, UserAgent userAgent)
    {
      OS = os;
      Device = device;
      UserAgent = userAgent;
    }

    /// <summary>
    /// Represents a collective string representation of the client information
    /// </summary>
    /// <returns>A string representation of the client information</returns>
    public override string ToString()
    {
      return string.Format("{0} {1} {2}", OS, Device, UserAgent);
    }
  }
}
