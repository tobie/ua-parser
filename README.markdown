ua-parser [![Build Status](https://secure.travis-ci.org/tobie/ua-parser.png?branch=master)](https://travis-ci.org/tobie/ua-parser)
=========

`ua-parser` is a multi-language port of [BrowserScope][2]'s [user agent string parser][3].

The crux of the original parser--the data collected by [Steve Souders][4] over the years--has been extracted into a separate [YAML file][5] so as to be reusable _as is_ by implementations in other programming languages. `ua-parser` is just a small wrapper around this data, along with ongoing improvements to the definitions.

Note that `ua-parser` has now been split out into multiple, distinct repositories, one for the [core definitions][6] and one for each [language implementation][7]. Patches and issues should be raised at those repositories, rather than this one.


[1]: http://nodejs.org
[2]: http://www.browserscope.org
[3]: http://code.google.com/p/ua-parser/
[4]: http://stevesouders.com/
[5]: https://raw.github.com/tobie/ua-parser/master/regexes.yaml
[6]: https://github.com/ua-parser/uap-core
[7]: https://github.com/ua-parser
