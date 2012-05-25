ua-parser
=========

`ua-parser` is a multi-language port of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.


Usage :: [node.js][1]
---------------------

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

    # Python won't do with dashes in module names so you'll need to rename,
    # copy, or symlink this directory and also add an __init__.py file,
    # like so:
    #
    # ln -s ua-parser ua_parser
    # touch ua_parser/__init__.py
    #
    # Now you're good to go.

    from ua_parser.py import user_agent_parser

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


Usage :: php
------------

    Please refer to the README in the php directory.


Usage :: php
------------

    Please refer to the README in the php directory.   


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

