ua_parser C# Library
======================

This is the CSharp implementation of [ua-parser](https://github.com/tobie/ua-parser).
The implementation uses the shared regex patterns and overrides from regexes.yaml. The assembly embeds the latest regex patterns which are loaded into the default parser. You can create a parser with more updated regex patterns by using the static methods on `Parser` to pass in specific patterns in yaml format.

Build and Run Tests:
------

    .\build.bat

Usage:
--------
```csharp
  using UAParser;

...

  string uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";

  // get a parser with the embedded regex patterns, methods exist for creating with provided regex patterns 
  var uaParser = Parser.GetDefault();

  Client c = uaParser.Parse(uaString);

  Console.WriteLine(c.UserAgent.Family); // => "Mobile Safari"
  Console.WriteLine(c.UserAgent.Major);  // => "5"
  Console.WriteLine(c.UserAgent.Minor);  // => "1"

  Console.WriteLine(c.OS.Family);        // => "iOS"
  Console.WriteLine(c.OS.Major);         // => "5"
  Console.WriteLine(c.OS.Minor);         // => "1"

  Console.WriteLine(c.Device.Family);    // => "iPhone"
```

Authors:
-------

  * Søren Enemærke [@sorenenemaerke](https://twitter.com/sorenenemaerke) / [github](https://https://github.com/enemaerke)
  * Atif Aziz [@raboof](https://twitter.com/raboof) / [github](https://https://github.com/atifaziz)

  