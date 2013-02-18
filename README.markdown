ua-parser [![Build Status](https://secure.travis-ci.org/tobie/ua-parser.png?branch=master)](https://travis-ci.org/tobie/ua-parser)
=========

`ua-parser` is a multi-language port of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.

Maintainers
-----------

* C#:         [Søren Enemærke ](https://github.com/enemaerke) ([@sorenenemaerke](https://twitter.com/sorenenemaerke))  
* D:          [Shripad K ](https://github.com/shripadk) ([@24shri](https://twitter.com/24shri))  
* Haskell:    [Ozgun Ataman ](https://github.com/ozataman) ([@ozataman](https://twitter.com/ozataman))  
* Java:       [Steve Jiang ](https://github.com/sjiang) ([@sjiang](https://twitter.com/sjiang))  
* JavaScript: [Tobie Langel ](https://github.com/tobie) ([@tobie](https://twitter.com/tobie))  
* Perl:       [Mamod Mehyar ](https://github.com/mamod) ([@mamod](https://twitter.com/mamod))  
* PHP:        [Dave Olsen ](https://github.com/dmolsen) ([@dmolsen](https://twitter.com/dmolsen))  
* Python:     [Lindsey Simon ](https://github.com/elsigh) ([@elsigh](https://twitter.com/elsigh))  

* `regexes.yaml`: Lindsey Simon & Tobie Langel

irc channel
-----------

[#ua-parser on freenode](irc://chat.freenode.net#ua-parse).

Contributing Changes to regexes.yaml
------------------------------------

Contributing to the project, especially `regexes.yaml`, is both welcomed and encouraged. To do so just do the following:

1. Fork the project
2. Create a branch for your changes
3. Modify `regexes.yaml` as appropriate
4. Add tests to the following files and follow their format:
    * `test_resources/test_device.yaml`
    * `test_resources/test_user_agent_parser.yaml`
    * `test_resources/test_user_agent_parser_os.yaml`
5. Push your branch to GitHub and submit a pull request
6. Monitor the pull request to make sure the Travis build succeeds. If it fails simply make the necessary changes to your branch and push it. Travis will re-test the changes.

That's it. If you don't feel comfortable forking the project or modifying the YAML you can also [submit an issue](https://github.com/tobie/ua-parser/issues) that includes the appropriate user agent string and the expected results of parsing.

Other ua-parser Libraries
-------------------------

There are a few other libraries which make use of ua-parser's patterns. These include:

* Ruby - [user_agent_parser](https://github.com/toolmantim/user_agent_parser)

Usage :: [node.js][1]
---------------------
```js
var r = require('ua-parser').parse(navigator.userAgent);

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
```

Note if you're only interested in one of the `ua`, `device` or `os` objects, you will getter better performance by using the more specific methods (`uaParser.parseUA`, `uaParser.parseOS` and `uaParser.parseDevice` respectively), e.g.:

```js
var p = require('ua-parser');

console.log(p.parseUA(navigator.userAgent).toString());
// -> "Safari 5.0.1"
console.log(p.parseOS(navigator.userAgent).toString());
// -> "iOS 5.1"
console.log(p.parseDevice(navigator.userAgent).toString());
// -> "iPhone"
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
# {'is_spider': False, 'is_mobile': True, 'family': 'iPhone'}
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
  System.out.println(c.device.isMobile);  // => true
  System.out.println(c.device.isSpider);  // => false
```


Usage :: php
------------

```php
require("uaparser.php");

$ua = "Mozilla/5.0 (Macintosh; Intel Ma...";

$parser = new UAParser;
$result = $parser->parse($ua);

print $result->ua->family;                // Safari
print $result->ua->major;                 // 6
print $result->ua->minor;                 // 0
print $result->ua->patch;                 // 2
print $result->ua->toString;              // Safari 6.0.2
print $result->ua->toVersionString;       // 6.0.2

print $result->os->family;                // Mac OS X
print $result->os->major;                 // 10
print $result->os->minor;                 // 7
print $result->os->patch;                 // 5
print $result->os->patch_minor;           // [null]
print $result->os->toString;              // Mac OS X 10.7.5
print $result->os->toVersionString;       // 10.7.5

print $result->device->family;            // Other

print $result->toFullString;              // Safari 6.0.2/Mac OS X 10.7.5
print $result->uaOriginal;                // Mozilla/5.0 (Macintosh; Intel Ma...
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
```haskell
{-

Install via Hackage and cabal like so:

cabal update
cabal install ua-parser
Now you're good to go.

-}

{-# LANGUAGE OverloadedStrings #-}

module Main where

import Web.UAParser

-- A test string
test_string = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B179 Safari/7534.48.3"

-- Main entry point for Haskell
main = do
    ua <- loadUAParser
    let uaResult = parseUA ua test_string
    print uaResult

    let osResult = parseOS ua test_string
    print osResult

-- Result from user agent parse
Just (UAResult {uarFamily = "Mobile Safari", uarV1 = Just "5", uarV2 = Just "1", uarV3 = Nothing})

-- Result from operating system parse
Just (OSResult {osrFamily = "iOS", osrV1 = Just "5", osrV2 = Just "1", osrV3 = Nothing, osrV4 = Nothing})
```

Plesae refer to Haddocks for more info; the API is pretty straightforward.

License
-------

The data contained in `regexes.yaml` is Copyright 2009 Google Inc. and available under the [Apache License, Version 2.0][6].

The original python code is Copyright 2008 Google Inc. and is available under the [Apache License, Version 2.0][7].

The JS port is Copyright 2010 Tobie Langel and is available under [your choice of MIT or Apache Version 2.0 license][8].

The PHP port is Copyright (c) 2011-2012 Dave Olsen and is available under the [MIT license][9].

The Java port is Copyright (c) 2012 Twitter, Inc and is available under the [Apache License, Version 2.0][6].

The D port is Copyright (c) 2012 Shripad K and is available under the [MIT license][10]. 

The C# port is Copyright (c) 2012 Søren Enemærke and is available under the [Apache License, Version 2.0][11].

The Perl port is Copyright (c) 2012 Mamod Mehyar and is available under the [Perl License, Version 5.10.1][12].

[1]: http://nodejs.org
[2]: http://www.browserscope.org
[3]: http://code.google.com/p/ua-parser/
[4]: http://stevesouders.com/
[5]: https://raw.github.com/tobie/ua-parser/master/regexes.yaml
[6]: http://www.apache.org/licenses/LICENSE-2.0
[7]: https://raw.github.com/tobie/ua-parser/master/py/LICENSE
[8]: https://raw.github.com/tobie/ua-parser/master/js/LICENSE
[9]: https://raw.github.com/tobie/ua-parser/master/php/LICENSE
[10]: https://raw.github.com/tobie/ua-parser/master/d/LICENSE
[11]: https://raw.github.com/tobie/ua-parser/master/csharp/LICENSE
[12]: http://dev.perl.org/licenses
