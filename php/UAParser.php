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

		// figure out the ua, os, and device properties if possible
		$result->ua           = $this->uaParser($ua);
		$result->os           = $this->osParser($ua);
		$result->device       = $this->deviceParser($ua);
		
		// create a full string version based on the ua and os objects
		$result->toFullString = $this->toFullString($result->ua, $result->os);
		

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
			
					
			
			}
			
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
				
				
				return $deviceObj;
			}
		}
		
		return $device;
		
	}
	
	/**
	 * Returns a string consisting of the family and full version number based on the provided type
	 * @param  object  the object (ua or os) to be used
	 * @return string  the result of combining family and version
	 */
	public function toString($obj) {
		
		$versionString = $this->toVersionString($obj);
		$string        = !empty($versionString) ? $obj->family.' '.$versionString : $obj->family;
		
		return $string;
	}
	
	/**
	 * Returns a string consisting of just the full version number based on the provided type
	 * @param  object  the obj that contains version number bits
	 * @return string  the result of combining the version number bits together
	 */
	public function toVersionString($obj) {
				
		$versionString = isset($obj->major) ? $obj->major : '';
		$versionString = isset($obj->minor) ? $versionString.'.'.$obj->minor : $versionString;
		$versionString = isset($obj->patch) ? $versionString.'.'.$obj->patch : $versionString;
		$versionString = isset($obj->patch_minor) ? $versionString.'.'.$obj->patch_minor : $versionString;
		
		return $versionString;
		
	}
	
	/**
	 * Returns a string consistig of the family and full version number for both the browser and os
	 * @param  object  the ua object
	 * @param  object  the os object
	 * @return string  the result of combining family and version
	 */
	public function toFullString($ua,$os) {
		
		$fullString = $this->toString($ua).'/'.$this->toString($os);
		
		return $fullString;
		
	}
	
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
