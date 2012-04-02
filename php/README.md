# ua-parser-php #

`ua-parser-php` is a PHP-based pseudo-port of the [ua-parser](http://code.google.com/p/ua-parser/) project. `ua-parser-php`
utilizes the user agents regex YAML file from ua-parser but otherwise creates its own set of attributes to describe a browser, OS, and device. `ua-parser-php`
was created as a new browser-detection library for the browser- and feature-detection library [Detector](https://github.com/dmolsen/Detector).

## Demo ##

You can [test ua-parser-php](http://uaparser.dmolsen.com/) with your browser.

## Usage ##

Straightforward:

    <?php

       require("UAParser.php");
       $result = UA::parse();

       print $result->full;
       // -> Chrome 16.0.912/Mac OS X 10.6.8

       print $result->browserFull;
       // -> "Chrome 16.0.912"
		
       print $result->browser;
       // -> "Chrome"
		
       print $result->version;
       // -> "16.0.912"
		
       print $result->major;
       // -> 16 (minor, build, & revision also available)
		
       print $result->osFull;
       // -> "Mac OS X 10.6.8"
		
       print $result->os;
       // -> "Mac OS X"
		
       print $result->osVersion;
       // -> "10.6.8"
		
       print $result->osMajor;
       // -> 10 (osMinor, osBuild, & osRevision also available)

       /* 
        * in select cases the device information will also be captured
        */

       print $result->deviceFull;
       // -> "Palm Pixi 1.0"
       
       print $result->device;
       // -> "Palm Pixi"

       print $result->deviceVersion
       // -> "1.0"

       print $result->deviceMajor;
       // -> 1 (deviceMinor also available)

       /*
        * Some other generic boolean options
        */

       print $result->isMobile;
       // -> (would return true if the browser met the criteria of a mobile browser based on the user agent information)

       print $result->isMobileDevice;
       // -> (would return true if the device met the criteria of a mobile device based on the user agent information)

       print $result->isTablet;
       // -> (would return true if the device was a tablet according to the user agent information)

       print $result->isSpider;
       // -> (would return true if the device was a spider according to the user agent information)

       print $result->isComputer;
       // -> (would return true if the device was a computer according to the user agent information)

       print $result->isUIWebview;
       // -> (would return true if the user agent was from a uiwebview in ios)

    ?>

If you want to grab a copy of the YAML data from ua-parser each night you can use a cron job and point it at the following bit of code:

    <?php

       require("UAParser.php");
       $result = UA::get();

    ?>

**NOTE:** This will overwrite quite a few changes I've made to the `user_agents_regex.yaml` file included with the `ua-parser-php` distribution.


## Credits ##

Thanks to the [ua-parser team](http://code.google.com/p/ua-parser/people/list) for making the YAML file available for others to build upon. Thanks to Bryan Shelton for some fixes.