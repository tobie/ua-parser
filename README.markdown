ua-parser
=========

`ua-parser` is a port to [node.js][1] of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [JSON file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.

Usage
-----

    var uaParser = require('ua-parser');
    var ua = uaParser.parse(navigator.userAgent);
    
    console.log(ua.tostring());
    // -> "Safari 5.0.1"
    
    console.log(ua.toVersionString());
    // -> "5.0.1"

    console.log(ua.toFullString());
    // -> "Safari 5.0.1/Mac OS X"
    
    console.log(ua.family);
    // -> "Safari"
    
    console.log(ua.major);
    // -> 5
    
    console.log(ua.minor);
    // -> 0
    
    console.log(ua.patch);
    // -> 1

    console.log(ua.os);
    // -> Mac OS X

License
-------

Your choice of [MIT][6] or [Apache License, Version 2.0][7] for the JS code which is Copyright 2010 Tobie Langel.

The data contained in `regexes.json` is Copyright 2009 Google Inc. and available under the [Apache License, Version 2.0][7].

[1]: http://node.js
[2]: http://www.browserscope.org
[3]: http://code.google.com/p/ua-parser/
[4]: http://stevesouders.com/
[5]: http://code.google.com/p/ua-parser/source/browse/trunk/regexes.json
[6]: http://github.com/tobie/ua-parser/raw/master/LICENSE
[7]: http://www.apache.org/licenses/LICENSE-2.0

