<?php

/*!
 * ua-parser-php v1.4.0
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 * Licensed under the MIT license
 *
 * ua-parser-php is the PHP library for the ua-parser project. Learn more about the ua-parser project at:
 * 
 * https://github.com/tobie/ua-parser
 *
 * The user agents data from the ua-parser project is licensed under the Apache license.
 * spyc-0.5, for loading the YAML, is licensed under the MIT license.
 * The initial list of generic feature phones & smartphones came from Mobile Web OSP under the MIT license
 * The initial list of spiders was taken from Yiibu's profile project under the MIT license.
 *
 */

// address 5.2 compatibility
if (!defined('__DIR__')) define('__DIR__', dirname(__FILE__));
if (!function_exists('json_decode') || !function_exists('json_encode')) {
	require_once(__DIR__."/lib/json/jsonwrapper.php");
}

// load spyc as a YAML loader
require_once(__DIR__."/lib/spyc-0.5/spyc.php");

class UA {
	
	private static $ua;
	private static $accept;
	private static $regexes;
	
	private static $debug    = false; // log requests
	public static $silent    = false; // no output when running UA::get()
	public static $nobackup  = false; // don't create a back-up when running UA::get()
	
	/**
	* Sets up some standard variables as well as starts the user agent parsing process
	*
	* @return {Object}       the result of the user agent parsing
	*/
	public static function parse($ua = NULL) {
		
		self::$ua      = $ua ? $ua : strip_tags($_SERVER["HTTP_USER_AGENT"]);
		self::$accept  = strip_tags($_SERVER["HTTP_ACCEPT"]);
		if (file_exists(__DIR__."/resources/regexes.yaml")) {
			self::$regexes = Spyc::YAMLLoad(__DIR__."/resources/regexes.yaml");
		} else {
			print "<h1>Error</h1>
				   <p>Please download the regexes.yaml file before using UAParser.php.</p>
				   <p>You can type the following at the command line to download the latest version:</p>
				   <blockquote>
					<code>%: cd /path/to/UAParser</code><br />
				   	<code>%: php UAParser.php -get</code>
				   </blockquote>";
			exit;
		}
	
		
		// run the regexes to match things up
		$uaRegexes = self::$regexes['user_agent_parsers'];
		foreach ($uaRegexes as $uaRegex) {
			if ($result = self::uaParser($uaRegex)) {
				$result->uaOriginal = self::$ua;
				break;
			}
		}
		
		// if no browser was found check to see if it can be matched at least against a device (e.g. spider, generic feature phone or generic smartphone)
		if (!$result) {
			if (($result = self::deviceParser()) && ($result->device != 'Spider')) {
				$result->isMobile       = true;
				$result->isMobileDevice = true;	
				$result->uaOriginal     = self::$ua;
			} else if (isset($result) && isset($result->device) && ($result->device == "Spider")) {
				$result->isMobile       = false;
				$result->isSpider       = true;
				$result->uaOriginal     = self::$ua;
			}
		}
		
		// still false?! see if it's a really dumb feature phone, if not just mark it as unknown
		if (!$result) {
			if ((strpos(self::$accept,'text/vnd.wap.wml') > 0) || (strpos(self::$accept,'application/vnd.wap.xhtml+xml') > 0) || isset($_SERVER['HTTP_X_WAP_PROFILE']) || isset($_SERVER['HTTP_PROFILE'])) {
				$result = new stdClass();
				$result->device         = "Generic Feature Phone";
				$result->deviceFull     = "Generic Feature Phone";
				$result->isMobile       = true;
				$result->isMobileDevice = true;
				$result->uaOriginal     = self::$ua;
			} else {
				$result = new stdClass();
				$result->device         = "Unknown";
				$result->deviceFull     = "Unknown";
				$result->isMobile       = false;
				$result->isMobileDevice = false;
				$result->isComputer     = true;
				$result->uaOriginal     = self::$ua;
			}
		}

		// log the results when testing
		if (self::$debug) {
			self::log($result);
		}
		
		return $result;
	}
	
	/**
	* Attemps to see if the user agent matches the regex for this test. If so it populates an obj
	* with properties based on the user agent. Will also try to fetch OS & device properties
	*
	* @param  {Array}        the regex to be tested as well as any extra variables that need to be swapped
	*
	* @return {Object}       the result of the user agent parsing
	*/
	private static function uaParser($regex) {
		
		// tests the supplied regex against the user agent
		if (preg_match("/".str_replace("/","\/",$regex['regex'])."/",self::$ua,$matches)) {

			// Define safe parser defaults
			$defaults = array(
				'isMobileDevice' => false,
				'isMobile' => false,
				'isSpider' => false,
				'isTablet' => false,
				'isComputer' => true,
			);
			// build the obj that will be returned starting with defaults
			$obj = (object) $defaults;

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
				$obj->build = $matches[4];
			}
			if (isset($matches[5])) {
				$obj->revision = $matches[5];
			}
			
			// pull out the browser family. replace the version number if necessary
			$obj->browser = isset($regex['family_replacement']) ? str_replace("$1",$obj->major,$regex['family_replacement']) : $matches[1];
			
			// set-up a clean version number
			$obj->version = isset($obj->major) ? $obj->major : "";
			$obj->version = isset($obj->minor) ? $obj->version.'.'.$obj->minor : $obj->version;
			$obj->version = isset($obj->build) ? $obj->version.'.'.$obj->build : $obj->version;
			$obj->version = isset($obj->revision) ? $obj->version.'.'.$obj->revision : $obj->version;
			
			// prettify
			$obj->browserFull = $obj->browser;
			if ($obj->version != '') {
				$obj->browserFull .= " ".$obj->version;
			}
			
			// detect if this is a uiwebview call on iOS
			$obj->isUIWebview = (($obj->browser == 'Mobile Safari') && !strstr(self::$ua,'Safari')) ? true : false;
			
			// check to see if this is a mobile browser
			$mobileBrowsers = array("Firefox Mobile","Opera Mobile","Opera Mini","Mobile Safari","webOS","IE Mobile","Playstation Portable",
			                        "Nokia","Blackberry","Palm","Silk","Android","Maemo","Obigo","Netfront","AvantGo","Teleca","SEMC-Browser",
			                        "Bolt","Iris","UP.Browser","Symphony","Minimo","Bunjaloo","Jasmine","Dolfin","Polaris","BREW","Chrome Mobile",
									"UC Browser","Tizen Browser");
			foreach($mobileBrowsers as $mobileBrowser) {
				if (stristr($obj->browser, $mobileBrowser)) {
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
				$obj->full = $obj->browserFull."/".$obj->osFull;
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
			
		} else {
			return false;
		}
	}
	
	/**
	* If the user agent is matched in uaParser() it also tries to check the OS and get properties
	*
	* @return {Object}       the result of the os parsing
	*/
	private static function osParser() {
		
		// build the obj that will be returned
		$osObj = new stdClass();
		
		// run the regexes to match things up
		$osRegexes = self::$regexes['os_parsers'];
		foreach ($osRegexes as $osRegex) {
			if (preg_match("/".str_replace("/","\/",$osRegex['regex'])."/",self::$ua,$matches)) {
				
				// Make sure matches 2 and 3 are at least set to null for setting
				// Major and Minor defaults
				if (!isset($matches[2])) { $matches[2] = null; }
				if (!isset($matches[3])) { $matches[3] = null; }

				// basic properties
				$osObj->osMajor   = isset($osRegex['os_v1_replacement']) ? $osRegex['os_v1_replacement'] : $matches[2];
				$osObj->osMinor   = isset($osRegex['os_v2_replacement']) ? $osRegex['os_v2_replacement'] : $matches[3];
				if (isset($matches[4])) {
					$osObj->osBuild = $matches[4];
				}
				if (isset($matches[5])) {
					$osObj->osRevision = $matches[5];
				}
				$osObj->os        = isset($osRegex['os_replacement'])    ? str_replace("$1",$osObj->osMajor,$osRegex['os_replacement'])  : $matches[1];

				// os version
				$osObj->osVersion = isset($osObj->osMajor) ? $osObj->osMajor : "";
				$osObj->osVersion = isset($osObj->osMinor) ? $osObj->osVersion.'.'.$osObj->osMinor : $osObj->osVersion;
				$osObj->osVersion = isset($osObj->osBuild) ? $osObj->osVersion.'.'.$osObj->osBuild : $osObj->osVersion;
				$osObj->osVersion = isset($osObj->osRevision) ? $osObj->osVersion.'.'.$osObj->osRevision : $osObj->osVersion; 
				
				// prettify
				$osObj->osFull    = $osObj->os." ".$osObj->osVersion;
				
				return $osObj;
			}
		}
		return false;
	}
	
	/**
	* If the user agent is matched in uaParser() it also tries to check the device and get its properties
	*
	* @return {Object}       the result of the device parsing
	*/
	private static function deviceParser() {
		
		// build the obj that will be returned
		$deviceObj = new stdClass();
		
		// run the regexes to match things up
		$deviceRegexes = self::$regexes['device_parsers'];
		foreach ($deviceRegexes as $deviceRegex) {
			if (preg_match("/".str_replace("/","\/",$deviceRegex['regex'])."/i",self::$ua,$matches)) {
				
				// Make sure matches 2 and 3 are at least set to null for setting
				// Major and Minor defaults
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
				$deviceObj->isMobileDevice = false;
				$mobileDevices  = array("iPhone","iPod","iPad","HTC","Kindle","Lumia","Amoi","Asus","Bird","Dell","DoCoMo","Huawei","i-mate","Kyocera",
				                        "Lenovo","LG","Kin","Motorola","Philips","Samsung","Softbank","Palm","HP ","Generic Feature Phone","Generic Smartphone");
				foreach($mobileDevices as $mobileDevice) {
					if (stristr($deviceObj->device, $mobileDevice)) {
						$deviceObj->isMobileDevice = true;
						break;
					}
				}

				// check to see if this is a tablet (not perfect)
				$deviceObj->isTablet = false;
				$tablets = array("Kindle","iPad","Playbook","TouchPad","Dell Streak","Galaxy Tab","Xoom");
				foreach($tablets as $tablet) {
					if (stristr($deviceObj->device, $tablet)) {
						$deviceObj->isTablet = true;
						break;
					}
				}
				
				return $deviceObj;
			}
		}
		return false;
	}
	
	/**
	* Logs the user agent info
	*/
	private static function log($data) {
		if (!$data) {
			$data = new stdClass();
			$data->ua = self::$ua;
		} 
		$jsonData = json_encode($data);
		$fp = fopen(__DIR__."/log/user_agents.log", "a");
		fwrite($fp, $jsonData."\r\n");
		fclose($fp);
	}
	
	/**
	* Gets the latest user agent. Back-ups the old version first. it will fail silently if something is wrong...
	*/
	public static function get() {
		if ($data = @file_get_contents("https://raw.github.com/tobie/ua-parser/master/regexes.yaml")) {
			if (file_exists(__DIR__."/resources/regexes.yaml")) {
				if (!self::$nobackup) { 
					if (!self::$silent) { print("backing up old YAML file...\n"); }
					if (!copy(__DIR__."/resources/regexes.yaml", __DIR__."/resources/regexes.".date("Ymdhis").".yaml")) {
						if (!self::$silent) { print("back-up failed...\n"); }
						exit;
					}
				}
			}
			$fp = fopen(__DIR__."/resources/regexes.yaml", "w");
			fwrite($fp, $data);
			fclose($fp);
			if (!self::$silent) { print("success...\n"); }
		} else {
			if (!self::$silent) { print("failed to get the file...\n"); }
		}
	}
}

if (defined('STDIN') && isset($argv) && ($argv[1] == '-get')) {
	UA::$silent   = ((isset($argv[2]) && ($argv[2] == '-silent')) || (isset($argv[3]) && ($argv[3] == '-silent'))) ? true : UA::$silent;
	UA::$nobackup = ((isset($argv[2]) && ($argv[2] == '-nobackup')) || (isset($argv[3]) && ($argv[3] == '-nobackup'))) ? true : UA::$nobackup;
	if (!UA::$silent) { print("getting the YAML file...\n"); }
	UA::get();
}

?>