namespace UAParser.ConsoleApp
{
  using System;
  using System.Linq;

  static class Program
  {
    static void Main(string[] args)
    {
      if (args.Any(arg => arg == "-?" || arg == "-h" || arg == "--help"))
      {
          Help();
          return;
      }

      var uaParser = Parser.GetDefault();
      string uaString;
      while ((uaString = Console.In.ReadLine()) != null)
      {
          uaString = uaString.Trim();
          if (uaString.Length == 0)
            continue;
          var c = uaParser.Parse(uaString);
          Console.WriteLine("Agent : {0}", c.UserAgent);
          Console.WriteLine("OS    : {0}", c.OS);
          Console.WriteLine("Device: {0}", c.Device);
      }
    }

    static void Help()
    {
            Console.WriteLine(@"UAParser
Copyright 2012 " + "S\u00f8ren Enem\u00e6rke" + @"
https://github.com/tobie/ua-parser

This application accepts user agent strings (one per line) from standard 
input, parses them and then emits the identified agent, operating system and 
device for each string.");
    }
  }
}
