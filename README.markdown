# ua-parser has moved

This project has moved to a new project space [ua-parser](https://github.com/ua-parser), separating the regexes from the parsers for the different languages into their own repos:

* [uap-core](https://github.com/ua-parser/uap-core) : The regex file necessary to build language ports of Browserscope's user agent parser.
* [uap-cpp](https://github.com/ua-parser/uap-cpp) : C++ implementation of ua-parser
* [uap-csharp](https://github.com/ua-parser/uap-csharp) : C# implementation of ua-parser
* [uap-d](https://github.com/ua-parser/uap-d) : D implementation of ua-parser
* [uap-go](https://github.com/ua-parser/uap-go) : Go implementation of ua-parser
* [uap-haskell](https://github.com/ua-parser/uap-haskell) : Haskell implementation of ua-parser
* [uap-java](https://github.com/ua-parser/uap-java) : Java implementation of ua-parser
* [uap-perl](https://github.com/ua-parser/uap-perl) : Perl implementation of ua-parser
* [uap-php](https://github.com/ua-parser/uap-php) : PHP implementation of ua-parser
* [uap-pig](https://github.com/ua-parser/uap-pig) : Pig implementation of ua-parser
* [uap-python](https://github.com/ua-parser/uap-python) : Python implementation of ua-parser
* [uap-r](https://github.com/ua-parser/uap-r) : R implementation of ua-parser
* [uap-ruby](https://github.com/ua-parser/uap-ruby) : A simple, comprehensive Ruby gem for parsing user agent strings with the help of BrowserScope's UA database
* [uap-ref-impl](https://github.com/ua-parser/uap-ref-impl) : JavaScript reference implementation of ua-parser.

**Please contribute to the respective repositories! Thanks.**

----

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
