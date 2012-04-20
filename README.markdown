ua-parser
=========

`ua-parser` is a port to [node.js][1] and python of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.

Usage
-----

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


Usage :: python
---------------

    # Python won't do with dashes in module names so you'll need to rename
    # this directory. i.e. mv ua-parser ua_parser
    from ua_parser.py import user_agent_parser

    # On the server, let's say you have a WebOB request object.
    user_agent_string = request.META.get('HTTP_USER_AGENT')
    family, v1, v2, v3 = user_agent_parser.Parse(user_agent_string)


Usage :: php
------------

    Refer to the README in the `php` directory.   


License
-------

The data contained in `regexes.yaml` is Copyright 2009 Google Inc. and available under the [Apache License, Version 2.0][6].

The original python code is Copyright 2008 Google Inc. and is available under the [Apache License, Version 2.0][7].

The JS port is Copyright 2010 Tobie Langel and is available under [your choice of MIT or Apache Version 2.0 license][8].

The PHP port is Copyright (c) 2011-2012 Dave Olsen and is available under the [MIT license][9].

[1]: http://node.js
[2]: http://www.browserscope.org
[3]: http://code.google.com/p/ua-parser/
[4]: http://stevesouders.com/
[5]: https://raw.github.com/tobie/ua-parser/master/regexes.yaml
[6]: http://www.apache.org/licenses/LICENSE-2.0
[7]: https://raw.github.com/tobie/ua-parser/master/py/LICENSE
[8]: https://raw.github.com/tobie/ua-parser/master/js/LICENSE
[9]: https://raw.github.com/tobie/ua-parser/master/php/LICENSE

