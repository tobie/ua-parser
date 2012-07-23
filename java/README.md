ua_parser Java Library
======================

This is the Java implementation of [ua-parser](https://github.com/tobie/ua-parser).  
The implementation uses the shared regex patterns and overrides from regexes.yaml.

Usage:
--------
    import ua_parser.Parser;
    import ua_parser.UserAgent;
    import ua_parser.Device;
    import ua_parser.OS;

    ...

      Parser uaParser = new Parser();
      String uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";

      UserAgent u = uaParser.parseUserAgent(uaString);
      System.out.println(u.family); // => "Mobile Safari"
      System.out.println(u.major);  // => "5"
      System.out.println(u.minor);  // => "1"

      OS o = uaParser.parseOS(uaString);
      System.out.println(o.family); // => "iOS"
      System.out.println(o.major);  // => "5"
      System.out.println(o.minor);  // => "1"

      Device d = uaParser.parseDevice(uaString);
      System.out.println(d.family);   // => "iPhone"
      System.out.println(d.isMobile); // => true
      System.out.println(d.isSpider); // => false


Build:
------

    mvn package

Author:
-------

  * Steve Jiang [@sjiang](https://twitter.com/sjiang)

  Based on the python implementation by Lindsey Simon and using agent data from BrowserScope
