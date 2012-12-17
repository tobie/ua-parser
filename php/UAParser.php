<?php

/*!
 * ua-parser-php v1.5.0
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 * Licensed under the MIT license
 *
 * ua-parser-php is the PHP library for the ua-parser project. Learn more about the ua-parser project at:
 * 
 *   https://github.com/tobie/ua-parser
 *
 * The user agents data from the ua-parser project is licensed under the Apache license.
 * spyc-0.5, for loading the YAML, is licensed under the MIT license.
 * The initial list of generic feature phones & smartphones came from Mobile Web OSP under the MIT license
 * The initial list of spiders was taken from Yiibu's profile project under the MIT license.
 *
 * Many thanks to the following major contributors:
 *
 *   - Bryan Shelton
 *   - Michael Bond
 *   - @rjd22 (https://github.com/rjd22)
 *   - Timo Tijhof (https://github.com/Krinkle)
 *   - Marcus Bointon (https://github.com/Synchro)
 *
 */

// address 5.1 compatibility
if (!function_exists('json_decode') || !function_exists('json_encode')) {
	require_once(dirname(__FILE__).DIRECTORY_SEPARATOR.'/lib/json/jsonwrapper.php');
}

class UA {
	
	private $regexes;
	private $debug = false;

	/**
	 * Start up the parser by importing the json file to $this->regexes
	 */
	public function __construct() {
		
		if (empty(self::$regexes)) {
			if (file_exists(__DIR__."/resources/regexes.yaml")) {
		if (file_exists(dirname(__FILE__).DIRECTORY_SEPARATOR.'resources/regexes.json')) {
			$this->regexes = json_decode(file_get_contents(dirname(__FILE__).DIRECTORY_SEPARATOR.'resources/regexes.json'));
		} else {
			$title        = 'Error loading ua-parser';
			$message      = 'Please download the regexes.json file before using UAParser.php. You can type the following at the command line to download the latest version: ';
			$instruction1 = '%: cd /path/to/UAParser/';
			$instruction2 = '%: php uaparser-cli.php -g';
			
			if (php_sapi_name() == 'cli') {
				print "\n".$title."\n";
				print $message."\n\n";
				print "    ".$instruction2."\n\n";
			} else {
				print '<html><head><title>'.$title.'</title></head><body>';
				print '<h1>'.$title.'</h1>';
				print '<p>'.$message.'</p>';
				print '<blockquote>';
				print '<code>'.$instruction1.'</code><br>';
				print '<code>'.$instruction2.'</code>';
				print '</blockquote>';
				print '</body></html>';
			}
			
			exit;
		}
	
	/**
	 * Sets up some standard variables as well as starts the user agent parsing process
	 * @param  string a user agent string to test, defaults to an empty string
	 * @return object the result of the user agent parsing
	 */
	public function parse($ua = '') {

		// build the default obj that will be returned
		$result = (object) array(
			'ua'           => (object) array(),
			'os'           => (object) array(),
			'device'       => (object) array(),
			'toFullString' => '',
			'uaOriginal'   => $ua
		);
		
			if ($uaObj = self::uaParser($uaRegex)) {
				$result = (object) array_merge((array) $result, (array) $uaObj);
				break;
			}
		}
		
		// if no browser was found check to see if it can be matched at least against a device (e.g. spider, generic feature phone or generic smartphone)
		if (!$uaObj) {
			if (($uaObj = self::deviceParser()) && ($uaObj->device != 'Spider')) {
				$result                 = (object) array_merge((array) $result, (array) $uaObj);
				$result->isMobile       = true;
				$result->isMobileDevice = true;	
				$result->uaOriginal     = self::$ua;
			} else if (isset($uaObj) && isset($uaObj->device) && ($uaObj->device == "Spider")) {
				$result                 = (object) array_merge((array) $result, (array) $uaObj);
				$result->isMobile       = false;
				$result->isSpider       = true;
				$result->uaOriginal     = self::$ua;
			}
		}
		
		// still false?! see if it's a really dumb feature phone, if not just mark it as unknown
		if (!$uaObj) {
			if ((strpos(self::$accept,'text/vnd.wap.wml') > 0) || (strpos(self::$accept,'application/vnd.wap.xhtml+xml') > 0) || isset($_SERVER['HTTP_X_WAP_PROFILE']) || isset($_SERVER['HTTP_PROFILE'])) {
				$result->device         = "Generic Feature Phone";
				$result->deviceFull     = "Generic Feature Phone";
				$result->isMobile       = true;
				$result->isMobileDevice = true;
				$result->uaOriginal     = self::$ua;
			} else {
				$result->device         = "Unknown";
				$result->deviceFull     = "Unknown";
				$result->isMobile       = false;
				$result->isMobileDevice = false;
				$result->isComputer     = true;
				$result->uaOriginal     = self::$ua;
			}
		}

		// Aliases
		$result->browser = $result->family;
		$result->build   = $result->patch;
		$result->osBuild = $result->osPatch;

		// log the results when testing
		if (self::$debug) {
			self::log($result);
		}
		
		return $result;
	}

	/**
	 * Attempts to see if the user agent matches a user_agents_parsers regex from regexes.json
	 * @param  string  a user agent string to test
	 * @return object  the result of the user agent parsing
	 */
	public function uaParser($uaString) {

		// tests the supplied regex against the user agent
		if (preg_match("/".str_replace("/","\/",str_replace("\/","/",$regex['regex']))."/", self::$ua, $matches)) {
			
		// build the default obj that will be returned
		$ua = (object) array(
				'family'          => 'Other',
				'major'           => null,
				'minor'           => null,
				'patch'           => null,
				'toString'        => '',
				'toVersionString' => ''
			  );
		// run the regexes to match things up
		$uaRegexes = $this->regexes->user_agent_parsers;
		foreach ($uaRegexes as $uaRegex) {
			
			// defaults
			$obj->isMobile       = false;
			$obj->isMobileDevice = false;

			// build the version numbers for the browser
			if (isset($matches[2]) || isset($regex['v1_replacement'])) {
				$obj->major  = isset($regex['v1_replacement']) ? $regex['v1_replacement'] : $matches[2];
			} else {
				$obj->major = '';
			}
			if (isset($matches[3]) || isset($regex['v2_replacement'])) {
				$obj->minor = isset($regex['v2_replacement']) ? $regex['v2_replacement'] : $matches[3];
			}
			if (isset($matches[4])) {
				$obj->patch = $matches[4];
			}
			if (isset($matches[5])) {
				$obj->revision = $matches[5];
			}
			
			// pull out the browser family. replace the version number if necessary
			if (isset($regex['family_replacement']) && strstr($regex['family_replacement'],"$1")) {
				$obj->family = str_replace("$1", $matches[1], $regex['family_replacement']);
			} else if (isset($regex['family_replacement'])) {
				$obj->family = $regex['family_replacement'];
			} else {
				$obj->family = $matches[1];
			}
			
			// set-up a clean version number
			$obj->version = isset($obj->major) ? $obj->major : "";
			$obj->version = isset($obj->minor) ? $obj->version.'.'.$obj->minor : $obj->version;
			$obj->version = isset($obj->patch) ? $obj->version.'.'.$obj->patch : $obj->version;
			$obj->version = isset($obj->revision) ? $obj->version.'.'.$obj->revision : $obj->version;
			
			// prettify
			$obj->browserFull = $obj->family;
			if ($obj->version != '') {
				$obj->browserFull .= " ".$obj->version;
			}
			
			// detect if this is a uiwebview call on iOS
			$obj->isUIWebview = (($obj->family == 'Mobile Safari') && !strstr(self::$ua, 'Safari')) ? true : false;
			
			// check to see if this is a mobile browser
			$mobileBrowsers = array('Firefox Mobile','Opera Mobile','Opera Mini','Mobile Safari','webOS','IE Mobile','Playstation Portable',
			                        'Nokia','Blackberry','Palm','Silk','Android','Maemo','Obigo','Netfront','AvantGo','Teleca','SEMC-Browser',
			                        'Bolt','Iris','UP.Browser','Symphony','Minimo','Bunjaloo','Jasmine','Dolfin','Polaris','BREW','Chrome Mobile',
			                        'UC Browser','Tizen Browser');
			foreach ($mobileBrowsers as $mobileBrowser) {
				if (stristr($obj->family, $mobileBrowser)) {
					$obj->isMobile = true;
					break;
				}
			}
					
			// figure out the OS for the browser, if possible
			if ($osObj = self::osParser()) {
				$obj = (object) array_merge((array) $obj, (array) $osObj);
			}
			
			// create an attribute combinining browser and os
			if (isset($obj->osFull) && $obj->osFull) {
				$obj->full = $obj->browserFull . "/" . $obj->osFull;
			}
			
			// figure out the device name for the browser, if possible
			if ($deviceObj = self::deviceParser()) {
				$obj = (object) array_merge((array) $obj, (array) $deviceObj);
			}
			
			// if this is a mobile browser make sure mobile device is set to true
			if ($obj->isMobile) {
				$obj->isMobileDevice = true; // this is going to catch android devices
			} else if ($obj->isMobileDevice) {
				$obj->isMobile = true;       // this will catch some weird htc devices
			}
			
			// if OS is Android check to see if this is a tablet. won't work on UA strings less than Android 3.0
			// based on: http://googlewebmastercentral.blogspot.com/2011/03/mo-better-to-also-detect-mobile-user.html
			// opera doesn't follow this convention though...
			if ((isset($obj->os) && $obj->os == 'Android') && !strstr(self::$ua, 'Mobile') && !strstr(self::$ua, 'Opera')) {
				$obj->isTablet = true;
				$obj->isMobile = false;
			}
			
			// some select mobile OSs report a desktop browser. make sure we note they're mobile
			$mobileOSs = array('Windows Phone 6.5','Windows CE','Symbian OS');
			if (isset($obj->os) && in_array($obj->os,$mobileOSs)) {
				$obj->isMobile       = true;
				$obj->isMobileDevice = true;
			}
			
			if (stristr(self::$ua,"tablet")) {
				$obj->isTablet       = true;
				$obj->isMobileDevice = true;
				$obj->isMobile       = false;
			}
			
			// record if this is a spider
			$obj->isSpider = (isset($obj->device) && $obj->device == "Spider") ? true : false;
			
			// record if this is a computer
			$obj->isComputer = (!$obj->isMobile && !$obj->isSpider && !$obj->isMobileDevice) ? true : false;

			return $obj;
		}

		return $ua;
		
	}

	/**
	 * Attempts to see if the user agent matches an os_parsers regex from regexes.json
	 * @param  string  a user agent string to test
	 * @return object  the result of the os parsing
	 */
	public function osParser($uaString) {
		
		// build the default obj that will be returned
		$os = (object) array(
				'family'          => 'Other',
				'major'           => null,
				'minor'           => null,
				'patch'           => null,
				'patch_minor'     => null,
				'toString'        => '',
				'toVersionString' => ''
		 	  );
		
		// run the regexes to match things up
		$osRegexes = $this->regexes->os_parsers;
		foreach ($osRegexes as $osRegex) {
			if (preg_match("/".str_replace("/","\/",str_replace("\/","/",$osRegex['regex']))."/",self::$ua,$matches)) {
				
				// Make sure matches 2 and 3 are at least set to null for setting
				// Major and Minor defaults
				if (!isset($matches[1])) { $matches[1] = null; }
				if (!isset($matches[2])) { $matches[2] = null; }
				if (!isset($matches[3])) { $matches[3] = null; }

				// basic properties
				$osObj->osMajor   = isset($osRegex['os_v1_replacement']) ? $osRegex['os_v1_replacement'] : $matches[2];
				$osObj->osMinor   = isset($osRegex['os_v2_replacement']) ? $osRegex['os_v2_replacement'] : $matches[3];
				if (isset($matches[4])) {
					$osObj->osPatch = $matches[4];
				}
				if (isset($matches[5])) {
					$osObj->osRevision = $matches[5];
				}
				$osObj->os        = isset($osRegex['os_replacement'])    ? str_replace("$1",$osObj->osMajor,$osRegex['os_replacement'])  : $matches[1];

				// os version
				$osObj->osVersion = isset($osObj->osMajor) ? $osObj->osMajor : "";
				$osObj->osVersion = isset($osObj->osMinor) ? $osObj->osVersion.'.'.$osObj->osMinor : $osObj->osVersion;
				$osObj->osVersion = isset($osObj->osPatch) ? $osObj->osVersion.'.'.$osObj->osPatch : $osObj->osVersion;
				$osObj->osVersion = isset($osObj->osRevision) ? $osObj->osVersion.'.'.$osObj->osRevision : $osObj->osVersion; 
				
				// prettify
				$osObj->osFull    = $osObj->os." ".$osObj->osVersion;
				
				return $osObj;
			}
		}

		return $os;
		
	}

	/**
	 * Attempts to see if the user agent matches a device_parsers regex from regexes.json
	 * @param  string  a user agent string to test
	 * @return object  the result of the device parsing
	 */
	public function deviceParser($uaString) {
		
		
		// defaults
		$deviceObj->isMobileDevice = false;
		$deviceObj->isTablet       = false;
		// build the default obj that will be returned
		$device = (object) array(
					'family' => 'Other'
				  );
		
		// run the regexes to match things up
		$deviceRegexes = $this->regexes->device_parsers;
		foreach ($deviceRegexes as $deviceRegex) {
			if (preg_match("/".str_replace("/","\/",str_replace("\/","/",$deviceRegex['regex']))."/i",self::$ua,$matches)) {
				
				// Make sure device matches are null
				// Device Name, Major and Minor defaults
				if (!isset($matches[1])) { $matches[1] = null; }
				if (!isset($matches[2])) { $matches[2] = null; }
				if (!isset($matches[3])) { $matches[3] = null; }

				// basic properties
				$deviceObj->deviceMajor  = isset($deviceRegex['device_v1_replacement']) ? $deviceRegex['device_v1_replacement'] : $matches[2];
				$deviceObj->deviceMinor  = isset($deviceRegex['device_v2_replacement']) ? $deviceRegex['device_v2_replacement'] : $matches[3];
				$deviceObj->device       = isset($deviceRegex['device_replacement']) ? str_replace("$1",$matches[1],$deviceRegex['device_replacement']) : str_replace("_"," ",$matches[1]);
				
				// device version?
				$deviceObj->deviceVersion = isset($deviceObj->deviceMajor) ? $deviceObj->deviceMajor : "";
				$deviceObj->deviceVersion = isset($deviceObj->deviceMinor) ? $deviceObj->deviceVersion.'.'.$deviceObj->deviceMinor : $deviceObj->deviceVersion;
				
				// prettify
				$deviceObj->deviceFull = $deviceObj->device." ".$deviceObj->deviceVersion;
				
				// check to see if this is a mobile device
				// this isn't really needed because if it matches a mobile browser it'll automatically mark it as a mobile device
				$mobileDevices  = array("iPhone","iPod","iPad","HTC","Kindle","Lumia","Amoi","Asus","Bird","Dell","DoCoMo","Huawei","i-mate","Kyocera",
				                        "Lenovo","LG","Kin","Motorola","Philips","Samsung","Softbank","Palm","HP ","Generic Feature Phone","Generic Smartphone",
										"Nintendo DSi","Nintendo 3DS","PlayStation Vita");
				foreach ($mobileDevices as $mobileDevice) {
					if (stristr($deviceObj->device, $mobileDevice)) {
						$deviceObj->isMobileDevice = true;
						break;
					}
				}

				// check to see if this is a tablet (not perfect)
				$tablets = array("Kindle","iPad","Playbook","TouchPad","Dell Streak","Galaxy Tab","Xoom");
				foreach ($tablets as $tablet) {
					if (stristr($deviceObj->device, $tablet)) {
						$deviceObj->isTablet = true;
						break;
					}
				}
				
				return $deviceObj;
			}
		}
		
		return $device;
		
	}
	
	/**
	* Logs the user agent info
	*/
		if (!$data) {
			$data = new stdClass();
			$data->ua = self::$ua;
		} 
	private function log($data) {
		$jsonData = json_encode($data);
		$fp = fopen(__DIR__."/log/user_agents.log", "a");
		fwrite($fp, $jsonData."\r\n");
		fclose($fp);
	}
	
}
