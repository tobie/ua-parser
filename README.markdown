ua-parser
=========

`ua-parser` is a multi-language port of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.


Usage :: [node.js][1]
---------------------
```js
var uaParser = require('ua-parser');
var ua = uaParser.parse(navigator.userAgent);

console.log(ua.tostring());
// -> "Safari 5.0.1"

console.log(ua.toVersionString());
// -> "5.0.1"

console.log(ua.family);
// -> "Safari"

console.log(ua.major);
// -> 5

console.log(ua.minor);
// -> 0

console.log(ua.patch);
// -> 1
```


Usage :: python
---------------
```python
# Install this into python site_packages like so:
#
# python setup.py install
#
# Now you're good to go.

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
require("UAParser.php");
$ua = UA::parse();

print $ua->family;        // Chrome (can also use $ua->browser)
print $ua->major;         // 16
print $ua->minor;         // 0
print $ua->patch;         // 912 (can also use $ua->build)
print $ua->browserFull;   // Chrome 16.0.912
print $ua->version;       // 16.0.912

print $ua->os;            // Mac OS X
print $ua->osMajor;       // 10
print $ua->osMinor;       // 6
print $ua->osPatch;       // 8 (can also use $ua->osBuild)
print $ua->osFull;        // Mac OS X 10.6.8
print $ua->osVersion;     // 10.6.8

print $ua->full;          // Chrome 16.0.912/Mac OS X 10.6.8

// in select cases the device information will also be captured

print $ua->device;        // Palm Pixi
print $ua->deviceMajor;   // 1
print $ua->deviceMinor;   // 0
print $ua->deviceFull;    // Palm Pixi 1.0
print $ua->deviceVersion; // 1.0

// some other generic boolean options

print $ua->isMobile;      // true or false
print $ua->isSpider;      // true or false
print $ua->isComputer;    // true or false

More information is available in the README in the PHP directory
```

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


License
-------

The data contained in `regexes.yaml` is Copyright 2009 Google Inc. and available under the [Apache License, Version 2.0][6].

The original python code is Copyright 2008 Google Inc. and is available under the [Apache License, Version 2.0][7].

The JS port is Copyright 2010 Tobie Langel and is available under [your choice of MIT or Apache Version 2.0 license][8].

The PHP port is Copyright (c) 2011-2012 Dave Olsen and is available under the [MIT license][9].

The Java port is Copyright (c) 2012 Twitter, Inc and is available under the [Apache License, Version 2.0][6].

The D port is Copyright (c) 2012 Shripad K and is available under the [MIT license][10].

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

