# ua-parser PHP Library #

This is the PHP library for the [ua-parser](https://github.com/tobie/ua-parser) project.

## v2.0 Changes ##

v2.0 of the PHP library, released in December 2012, marked a huge transition from the previous pseudo-port of `ua-parser` to a true port that matches up well with the other libraries in the `ua-parser` repo. The primary changes:

* the PHP library is now dynamic
* properties are nested _(e.g. $result->family is now $result->ua->family)_
* parse() now expects a user agent string to be passed no matter what
* `uaParser()`, `osParser()`, and `deviceParser()` are public and can be used just to return select bits
* the `is*` boolean properties _(e.g. isMobile)_ have been dropped. they now exist as part of the `ua-classifier` project.

## Demo ##

You can [test the PHP library](http://uaparser.dmolsen.com/) with your browser.

## Usage ##

Straightforward:

```php
require("UAParser.php");

$ua = "Mozilla/5.0 (Macintosh; Intel Ma...";

$parser = new UA;
$result = $parser->parse($ua);

print $result->ua->family;         			  // Safari
print $result->ua->major;          			  // 6
print $result->ua->minor;          			  // 0
print $result->ua->patch;          			  // 2
print $result->ua->toString;    				  // Safari 6.0.2
print $result->ua->toVersionString;			  // 6.0.2

print $result->os->family;								// Mac OS X
print $result->os->major;									// 10
print $result->os->minor;									// 7
print $result->os->patch;									// 5
print $result->os->patch_minor;						// [null]
print $result->os->toString;							// Mac OS X 10.7.5
print $result->os->toVersionString;				// 10.7.5

print $result->device->family; 						// Other

print $result->toFullString;							// Safari 6.0.2/Mac OS X 10.7.5
print $result->uaOriginal;								// Mozilla/5.0 (Macintosh; Intel Ma...
```

## Using ua-parser PHP Library from the Command Line ##

A command line utility is now included with the PHP library. The following commands are supported:

### Get Usage Info

Provides simple usage information:

    php uaparser-cli.php

### Update the regexes.json File

Fetches an updated YAML file for UAParser and overwrites the current JSON file. You can use the following as part of a cron job that runs nightly. 

    php uaparser-cli.php -g [-s] [-n]
        
        By default is verbose. Use -s to turn that feature off.
        By default creates a back-up. Use -n to turn that feature off.

### Convert an existing regexes.yaml file to regexes.json

With the change to v2.0 you may have an existing and customized YAML file for ua-parser. Use the following to convert it to JSON.

		php uaparser-cli.php -c [-s] [-n]

				 By default is verbose. Use -s to turn that feature off.
				 By default creates a back-up. Use -n to turn that feature off.

### Parse an Apache Log File

Parses the supplied Apache log file to test UAParser.php. Saves the UA to a file when the UA or OS family aren't found or when the UA is listed as a generic smartphone or as a generic feature phone.

    php uaparser-cli.php -l "/path/to/apache/logfile"
        

### Parse a Single User Agent String

Parses a user agent string and dumps the results as a list.

    php uaparser-cli.php [-j] "your user agent string"
           
        Use the -j flag to print the result as JSON.

## Credits ##

Thanks to the [original ua-parser team](http://code.google.com/p/ua-parser/people/list) for making the YAML file available for others to build upon.

Also, many thanks to the following major contributors to the PHP library:

* Bryan Shelton
* Michael Bond
* @rjd22
* Timo Tijhof 
* Marcus Bointon
