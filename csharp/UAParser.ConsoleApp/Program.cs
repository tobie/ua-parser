using System;

namespace UAParser.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      String uaString = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_5; en-us) AppleWebKit/533.18.1 (KHTML, like Gecko) Version/5.0.2 Safari/533.18.5";

      Parser uaParser = Parser.GetDefault();

      ClientInfo c = uaParser.Parse(uaString);

      Console.WriteLine(c.UserAgent);  //Safari 5.0.2
      Console.WriteLine(c.OS); // Mac OS X 10.6.5
      Console.WriteLine(c.Device); //

      Console.ReadLine();
    }
  }
}
