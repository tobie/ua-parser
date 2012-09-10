ua-parser
=========

`ua-parser` is a multi-language port of [BrowserScope][1]'s [user agent string parser][2].

The crux of the original parser--the data collected by [Steve Souders][3] over the years--has been extracted into a separate [YAML file][4] so as to be reusable _as is_ by implementations in other programming languages.

`ua-parser` is just a small wrapper around this data.


Installation
---------------------

Install [DMD][5].
Install [D-YAML][6] and make sure you add the relevant paths to dmd.conf so the linker can find the module.

Once setup, you can directly include the source file (UaParser.d) in your working directory or you can generate a library and add the path to dmd.conf.

Usage
---------------

Please refer to the example.d file. To run the example, execute the following command in terminal:

`rdmd --force example.d`

[1]: http://www.browserscope.org
[2]: http://code.google.com/p/ua-parser/
[3]: http://stevesouders.com/
[4]: https://raw.github.com/tobie/ua-parser/master/regexes.yaml
[5]: http://dlang.org/download.html
[6]: https://github.com/kiith-sa/D-YAML/wiki/Getting-Started
