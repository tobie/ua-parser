# ua-parser PHP Library #

This is the PHP library for the [ua-parser](https://github.com/tobie/ua-parser) project. This library utilizes the user agents regex YAML file from ua-parser but otherwise creates its own set of attributes to describe a browser, OS, and device.

## Demo ##

You can [test the PHP library](http://uaparser.dmolsen.com/) with your browser.

## Usage ##

Straightforward:

```php
require("UAParser.php");
$ua = UA::parse();

print $ua->family;         // Chrome (can also use $ua->browser)
print $ua->major;          // 16
print $ua->minor;          // 0
print $ua->patch;          // 912 (can also use $ua->build)
print $ua->browserFull;    // Chrome 16.0.912
print $ua->version;        // 16.0.912

print $ua->os;             // Mac OS X
print $ua->osMajor;        // 10
print $ua->osMinor;        // 6
print $ua->osPatch;        // 8 (can also use $ua->osBuild)
print $ua->osFull;         // Mac OS X 10.6.8
print $ua->osVersion;      // 10.6.8

print $ua->full;           // Chrome 16.0.912/Mac OS X 10.6.8

// in select cases the device information will also be captured

print $ua->device;         // Palm Pixi
print $ua->deviceMajor;    // 1
print $ua->deviceMinor;    // 0
print $ua->deviceFull;     // Palm Pixi 1.0
print $ua->deviceVersion;  // 1.0

// some other generic boolean options

print $ua->isMobile;       // true or false
print $ua->isMobileDevice; // true or false
print $ua->isTablet;       // true or false
print $ua->isSpider;       // true or false
print $ua->isComputer;     // true or false
print $ua->isUIWebview;    // true or false, iOS-only
```

## Using ua-parser PHP Library from the Command Line ##

A command line utility is now included with the PHP library. The following commands are supported:

### Get Usage Info

Provides simple usage information:

    php uaparser-cli.php

### Update the regexes.yaml File

Fetches an updated YAML file for UAParser and overwrites the current file. You can use the following as part of a cron job that runs nightly. 

    php uaparser-cli.php -g [-s] [-n]
        
        By default is verbose. Use -s to turn that feature off.
        By default creates a back-up. Use -n to turn that feature off.

### Parse an Apache Log File

Parses the linked Apache log file to help determine which user agents might need to be added to regexes.yaml.

    php uaparser-cli.php -l /path/to/apache/logfile
        

### Parse a Single User Agent String

Parses a user agent string and dumps the results as a list.

    php uaparser-cli.php [-j] "your user agent string"
           
        Use the -j flag to print the result as JSON.

## Using ua-parser For a Redirect Script ##

It's very simple to use ua-parser as a redirect script. To do so do the following:

    <?php

    	// require the ua-parser-php library
    	require_once("/path/to/UAParser.php");

    	// parse the requesting user agent
    	$result = UA::parse();

    	// redirect phones, to redirect tablets use isMobileDevice
    	if ($result->isMobile) {
    		header("location:http://dmolsen.com");
    	}
	
	    // run through the rest of your code for the page for desktop devices & spiders
	
    ?>

You can use any of the properties above to perform the redirect but the boolean options are probably easiest.

## Credits ##

Thanks to the [original ua-parser team](http://code.google.com/p/ua-parser/people/list) for making the YAML file available for others to build upon.

Also, many thanks to the following major contributors to the PHP library:

* Bryan Shelton
* Michael Bond
* @rjd22
* Timo Tijhof 
* Marcus Bointon