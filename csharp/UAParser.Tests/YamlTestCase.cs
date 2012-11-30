using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UAParser.Tests
{
  public abstract class YamlTestCase
  {
    public string UserAgent { get; set; }
    public abstract void Verify(ClientInfo clientInfo);

    protected void AssertMatch<T>(T expected, T actual, string type)
    {
      if (typeof(T) == typeof(string))
      {
        string exp = expected as string;
        string act = actual as string;

        if (string.IsNullOrEmpty(exp) && string.IsNullOrEmpty(act))
          return;
      }

      Assert.True(expected.Equals(actual), type+" did not match. (expected:" + expected + " actual:" + actual + ")  in " + UserAgent);
    }
  }
}
