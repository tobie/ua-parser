using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UAParser.Tests
{
  public class ParserTests
  {
    [Fact]
    public void can_get_default_parser()
    {
      Parser parser = Parser.GetDefault();
    }
  }
}
