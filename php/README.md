# ua-parser PHP Library #

This is the PHP library for the [ua-parser](https://github.com/tobie/ua-parser) project. This library
utilizes the user agents regex YAML file from ua-parser but otherwise creates its own set of attributes to describe a browser, OS, and device. The 

## Demo ##

You can [test the PHP library](http://uaparser.dmolsen.com/) with your browser.

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

## Getting the User-Agent Data ##

To get the user-agent data for either the initial load of the project or each night as a cron job you can use the following on the command line:

    %: cd /path/to/project/
    %: php UAParser.php -get

Alternatively, you can create a PHP script that includes the following:

    <?php

			require("/path/to/UAParser.php");
			UA::get();
			
    ?>

## Credits ##

Thanks to the [ua-parser team](http://code.google.com/p/ua-parser/people/list) for making the YAML file available for others to build upon. Thanks to Bryan Shelton for some fixes.