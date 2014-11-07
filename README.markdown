ua-parser [![Build Status](https://secure.travis-ci.org/tobie/ua-parser.png?branch=master)](https://travis-ci.org/tobie/ua-parser)
=========

`ua-parser` is a multi-language port of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data, along with ongoing improvements to the definitions.

Maintainers
-----------

* C#: [Søren Enemærke](https://github.com/enemaerke) ([@sorenenemaerke](https://twitter.com/sorenenemaerke))
* D: [Shripad K](https://github.com/shripadk) ([@24shri](https://twitter.com/24shri))
* Go: [Yihuan Zhou](https://github.com/yihuanz) ([@yihuanz](https://twitter.com/yihuanz))
* Haskell: [Ozgun Ataman](https://github.com/ozataman) ([@ozataman](https://twitter.com/ozataman))
* Java: [Steve Jiang](https://github.com/sjiang) ([@sjiang](https://twitter.com/sjiang))
* JavaScript: [Tobie Langel](https://github.com/tobie) ([@tobie](https://twitter.com/tobie))
* Perl: [Mamod Mehyar](https://github.com/mamod) ([@mamod](https://twitter.com/mamod))
* PHP: [Lars Strojny](https://github.com/lstrojny) ([@lstrojny](https://twitter.com/lstrojny))
* Pig: [Niels Basjes](https://github.com/nielsbasjes) ([@nielsbasjes](https://twitter.com/nielsbasjes))
* Python: [Lindsey Simon](https://github.com/elsigh) ([@elsigh](https://twitter.com/elsigh))
* `regexes.yaml`: Lindsey Simon & Tobie Langel

Communication channels
-----------------------

* irc (#ua-parser on freenode) <irc://chat.freenode.net#ua-parser>
* [mailing list](https://groups.google.com/forum/#!forum/ua-parser)

Contributing Changes to regexes.yaml
------------------------------------

Please read the [contributors' guide](https://github.com/tobie/ua-parser/blob/master/CONTRIBUTING.md)

Other ua-parser Libraries
-------------------------

There are a few other libraries which make use of ua-parser's patterns. These include:

* Ruby - [user_agent_parser](https://github.com/toolmantim/user_agent_parser)

See ua-parser in action
-----------------------

[whatsmyua.info](http://www.whatsmyua.info) shows what ua-parser reports for
your current user agent/OS/device, or for any arbitrary user agent string.

Usage :: [node.js][1]
---------------------
```js
var http = require('http');

http.createServer(function (req, res) {

  var r = require('ua-parser').parse(req.headers['user-agent']);

  console.log(r.ua.toString());        // -> "Safari 5.0.1"
  console.log(r.ua.toVersionString()); // -> "5.0.1"
  console.log(r.ua.family)             // -> "Safari"
  console.log(r.ua.major);             // -> "5"
  console.log(r.ua.minor);             // -> "0"
  console.log(r.ua.patch);             // -> "1"

  console.log(r.os.toString());        // -> "iOS 5.1"
  console.log(r.os.toVersionString()); // -> "5.1"
  console.log(r.os.family)             // -> "iOS"
  console.log(r.os.major);             // -> "5"
  console.log(r.os.minor);             // -> "1"
  console.log(r.os.patch);             // -> null

  console.log(r.device.family);        // -> "iPhone"

}).listen(3000);
```

Note if you're only interested in one of the `ua`, `device` or `os` objects, you will getter better performance by using the more specific methods (`uaParser.parseUA`, `uaParser.parseOS` and `uaParser.parseDevice` respectively), e.g.:

```js
var http = require('http'),
    p = require('ua-parser');

http.createServer(function (req, res) {

  var userAgent = req.headers['user-agent'];

  console.log(p.parseUA(userAgent).toString());
  // -> "Safari 5.0.1"
  console.log(p.parseOS(userAgent).toString());
  // -> "iOS 5.1"
  console.log(p.parseDevice(userAgent).toString());
  // -> "iPhone"

}).listen(3000);
```

Usage :: python
---------------
You can install `ua-parser` by running:

```python
pip install pyyaml ua-parser
```

And here's how to use it:

```python
from ua_parser import user_agent_parser

# On the server, you could use a WebOB request object.
user_agent_string = request.META.get('HTTP_USER_AGENT')

# For demonstration purposes, though an iPhone ...
user_agent_string = 'Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B179 Safari/7534.48.3'

# Get back a big dictionary of all the goodies.
result_dict = user_agent_parser.Parse(user_agent_string)

print result_dict['user_agent']
# {'major': '5', 'minor': '1', 'family': 'Mobile Safari', 'patch': None}

print result_dict['os']
# {'major': '5', 'patch_minor': None, 'minor': '1', 'family': 'iOS', 'patch': None}

print result_dict['device']
# {'family': 'iPhone'}
```


Usage :: java
-------------
```java
import ua_parser.Parser;
import ua_parser.Client;

...

  String uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";

  Parser uaParser = new Parser();
  Client c = uaParser.parse(uaString);

  System.out.println(c.userAgent.family); // => "Mobile Safari"
  System.out.println(c.userAgent.major);  // => "5"
  System.out.println(c.userAgent.minor);  // => "1"

  System.out.println(c.os.family);        // => "iOS"
  System.out.println(c.os.major);         // => "5"
  System.out.println(c.os.minor);         // => "1"

  System.out.println(c.device.family);    // => "iPhone"
```


Usage :: Pig
-------------
For Pig there are UDFs for getting a single value and UDFs for getting a tuple with all values for either Device, Os of UserAgent.
For most usecases the tuple UDFs will be the most useful.

```pig
REGISTER ua-parser-pig-0.1-SNAPSHOT-job.jar

DEFINE Device           ua_parser.pig.Device;
DEFINE Os               ua_parser.pig.Os;
DEFINE UserAgent        ua_parser.pig.UserAgent;

UserAgents =
    Load 'useragents.txt' AS (useragent:chararray);

AgentSpecs =
    FOREACH UserAgents
    GENERATE
             Device(useragent)              AS Device,
             Os(useragent)                  AS Os,
             UserAgent(useragent)           AS UserAgent,

             useragent                      AS Useragent;

DESCRIBE AgentSpecs;
DUMP AgentSpecs;
```

The versions that return only a single value:

```pig
REGISTER ua-parser-pig-0.1-SNAPSHOT-job.jar

DEFINE DeviceFamily     ua_parser.pig.device.Family;
DEFINE OsFamily         ua_parser.pig.os.Family;
DEFINE OsMajor          ua_parser.pig.os.Major;
DEFINE OsMinor          ua_parser.pig.os.Minor;
DEFINE OsPatch          ua_parser.pig.os.Patch;
DEFINE OsPatchMinor     ua_parser.pig.os.PatchMinor;
DEFINE UseragentFamily  ua_parser.pig.useragent.Family;
DEFINE UseragentMajor   ua_parser.pig.useragent.Major;
DEFINE UseragentMinor   ua_parser.pig.useragent.Minor;
DEFINE UseragentPatch   ua_parser.pig.useragent.Patch;

UserAgents =
    Load 'useragents.txt' AS (useragent:chararray);

AgentSpecs =
    FOREACH  UserAgents
    GENERATE DeviceFamily(useragent)    AS DeviceFamily:chararray,

             OsFamily(useragent)        AS OsFamily:chararray,
             OsMajor(useragent)         AS OsMajor:chararray,
             OsMinor(useragent)         AS OsMinor:chararray,
             OsPatch(useragent)         AS OsPatch:chararray,
             OsPatchMinor(useragent)    AS OsPatchMinor:chararray,

             UseragentFamily(useragent) AS UseragentFamily:chararray,
             UseragentMajor(useragent)  AS UseragentMajor:chararray,
             UseragentMinor(useragent)  AS UseragentMinor:chararray,
             UseragentPatch(useragent)  AS UseragentPatch:chararray,

             useragent                  AS Useragent;

DUMP AgentSpecs;
```


Usage :: php
------------

```php
require_once 'vendor/autoload.php';
use UAParser\Parser;

$ua = "Mozilla/5.0 (Macintosh; Intel Ma...";

$parser = Parser::create();
$result = $parser->parse($ua);

print $result->ua->family;            // Safari
print $result->ua->major;             // 6
print $result->ua->minor;             // 0
print $result->ua->patch;             // 2
print $result->ua->toString();        // Safari 6.0.2
print $result->ua->toVersion();       // 6.0.2

print $result->os->family;            // Mac OS X
print $result->os->major;             // 10
print $result->os->minor;             // 7
print $result->os->patch;             // 5
print $result->os->patchMinor;        // [null]
print $result->os->toString();        // Mac OS X 10.7.5
print $result->os->toVersion();       // 10.7.5

print $result->device->family;        // Other

print $result->toString();            // Safari 6.0.2/Mac OS X 10.7.5
print $result->uaOriginal;            // Mozilla/5.0 (Macintosh; Intel Ma...
```

[More information is available in the README](https://github.com/tobie/ua-parser/tree/master/php) in the PHP directory

Usage :: D
-------------
```d
import UaParser;

import std.stdio;

void main() {
    auto agent = UaParser.parse("Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3");
    std.stdio.writeln(agent.browser.family); // Mobile Safari
    std.stdio.writeln(agent.browser.major);  // 5
    std.stdio.writeln(agent.browser.minor);  // 1
    std.stdio.writeln(agent.browser.patch);  // 0
    std.stdio.writeln(agent.browser.toString); // Mobile Safari 5.1.0
    std.stdio.writeln(agent.browser.toVersionString); // 5.1.0

    std.stdio.writeln(agent.os.family); // iOS
    std.stdio.writeln(agent.os.major);  // 5
    std.stdio.writeln(agent.os.minor);  // 1
    std.stdio.writeln(agent.os.patch);  // 1
    std.stdio.writeln(agent.os.toString); // iOS 5.1.1
    std.stdio.writeln(agent.os.toVersionString); // 5.1.1

    std.stdio.writeln(agent.toFullString); // Mobile Safari 5.1.0/iOS 5.1.1

    std.stdio.writeln(agent.device.family); // iPhone

    std.stdio.writeln(agent.isMobile); // true
    std.stdio.writeln(agent.isSpider); // false
}
```

Usage :: C#
-------------
Install the NuGet package

	Install-Package UAParser

Sample Usage:

```csharp
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
```

Usage :: Perl
-------------
```perl
use HTTP::UA::Parser;
my $r = HTTP::UA::Parser->new();

print $r->ua->toString();         # -> "Safari 5.0.1"
print $r->ua->toVersionString();  # -> "5.0.1"
print $r->ua->family;             # -> "Safari"
print $r->ua->major;              # -> "5"
print $r->ua->minor;              # -> "0"
print $r->ua->patch;              # -> "1"

print $r->os->toString();         # -> "iOS 5.1"
print $r->os->toVersionString();  # -> "5.1"
print $r->os->family              # -> "iOS"
print $r->os->major;              # -> "5"
print $r->os->minor;              # -> "1"
print $r->os->patch;              # -> undef

print $r->device->family;         # -> "iPhone"

More information is available in the README in the perl directory
```

Usage :: Haskell
---------------

Install the package:

    cabal update
    cabal install ua-parser

Sample Usage:

```haskell
{-# LANGUAGE OverloadedStrings #-}

module Main where

import Web.UAParser

test_string = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B179 Safari/7534.48.3"

main = do
    print $ parseUA test_string
    print $ parseOS test_string
```

Result of running this program:

```haskell
-- Result from user agent parse
Just (UAResult {uarFamily = "Mobile Safari", uarV1 = Just "5", uarV2 = Just "1", uarV3 = Nothing})

-- Result from operating system parse
Just (OSResult {osrFamily = "iOS", osrV1 = Just "5", osrV2 = Just "1", osrV3 = Nothing, osrV4 = Nothing})
```

Please refer to Haddocks for more info; the API is pretty straightforward.

Usage :: Go
------------

Install the package:

    go get "github.com/tobie/ua-parser/go/uaparser"

Sample Usage

```
package main

import (
  "github.com/tobie/ua-parser/go/uaparser"
  "fmt"
)

func main() {
  testStr := "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-us; Silk/1.1.0-80) AppleWebKit/533.16 (KHTML, like Gecko) Version/5.0 Safari/533.16 Silk-Accelerated=true"
  regexFile := "../../regexes.yaml"
  parser := uaparser.New(regexFile)
  client := parser.Parse(testStr)
  fmt.Println(client.UserAgent.Family)  // "Amazon Silk"
  fmt.Println(client.UserAgent.Major)   // "1"
  fmt.Println(client.UserAgent.Minor)   // "1"
  fmt.Println(client.UserAgent.Patch)   // "0-80"
  fmt.Println(client.Os.Family)         // "Android"
  fmt.Println(client.Os.Major)          // ""
  fmt.Println(client.Os.Minor)          // ""
  fmt.Println(client.Os.Patch)          // ""
  fmt.Println(client.Os.PatchMinor)     // ""
  fmt.Println(client.Device.Family)     // "Kindle Fire"
}
```

[More information is available in the README](https://github.com/tobie/ua-parser/tree/master/go/uaparser) in the Go directory

License
-------

The data contained in `regexes.yaml` is Copyright (c) 2009 Google Inc. and [other contributors][6], and available under the [Apache License, Version 2.0][7].

The original python code is Copyright (c) 2008 Google Inc. and [other contributors][8],and is available under the [Apache License, Version 2.0][9].

The JS port is Copyright (c) 2010 Tobie Langel and [other contributors][10], and is available under [your choice of MIT or Apache Version 2.0 license][11].

The PHP port is Copyright (c) 2011-2012 Dave Olsen and [other contributors][12], and is available under the [MIT license][13].

The Java port is Copyright (c) 2012 Twitter, Inc and [other contributors][14], and is available under the [Apache License, Version 2.0][7].

The D port is Copyright (c) 2012 Shripad K and [other contributors][15], and is available under the [MIT license][16].

The C# port is Copyright (c) 2012 Søren Enemærke and [other contributors][17], and is available under the [Apache License, Version 2.0][18].

The Perl port is Copyright (c) 2012 Mamod Mehyar and [other contributors][19], and is available under the [Perl License, Version 5.10.1][20].

The Pig port is Copyright (c) 2013 Niels Basjes and [other contributors][21], and is available under the [Apache License, Version 2.0][22].

The Go port is Copyright (c) 2013 Yihuan Zhou and [other contributors][23], and is available under the [MIT License][24].


[1]: http://nodejs.org
[2]: http://www.browserscope.org
[3]: http://code.google.com/p/ua-parser/
[4]: http://stevesouders.com/
[5]: https://raw.github.com/tobie/ua-parser/master/regexes.yaml
[6]: https://github.com/tobie/ua-parser/commits/master/regexes.yaml
[7]: http://www.apache.org/licenses/LICENSE-2.0
[8]: https://github.com/tobie/ua-parser/commits/master/py
[9]: https://raw.github.com/tobie/ua-parser/master/py/LICENSE
[10]: https://github.com/tobie/ua-parser/commits/master/js
[11]: https://raw.github.com/tobie/ua-parser/master/js/LICENSE
[12]: https://github.com/tobie/ua-parser/commits/master/php
[13]: https://raw.github.com/tobie/ua-parser/master/php/LICENSE
[14]: https://github.com/tobie/ua-parser/commits/master/java
[15]: https://github.com/tobie/ua-parser/commits/master/d
[16]: https://raw.github.com/tobie/ua-parser/master/d/LICENSE
[17]: https://github.com/tobie/ua-parser/commits/master/csharp
[18]: https://raw.github.com/tobie/ua-parser/master/csharp/LICENSE
[19]: https://github.com/tobie/ua-parser/commits/master/perl
[20]: http://dev.perl.org/licenses
[21]: https://github.com/tobie/ua-parser/commits/master/pig
[22]: https://raw.github.com/tobie/ua-parser/master/pig/LICENSE.txt
[23]: https://github.com/tobie/ua-parser/commits/master/go
[24]: https://raw.github.com/tobie/ua-parser/master/go/uaparser/LICENSE.md
