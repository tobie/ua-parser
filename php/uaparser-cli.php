<?php

/*!
 * ua-parser-php CLI v1.0.0
 *
 * Copyright (c) 2012 Dave Olsen, http://dmolsen.com
 * Licensed under the MIT license
 *
 * This is the CLI for ua-parser-php. The following commands are supported:
 *
 *   php uaparser-cli.php
 *       Provides the usage information.
 *
 *   php uaparser-cli.php -g [-s] [-n]
 *       Fetches an updated YAML file for UAParser and overwrites the current file.
 *       By default is verbose. Use -s to turn that feature off.
 *       By default creates a back-up. Use -n to turn that feature off.
 *
 *   php uaparser-cli.php -l /path/to/apache/logfile
 *       Parses the supplied Apache log file to test UAParser.php
 *
 *   php uaparser-cli.php [-j] "your user agent string"
 *       Parses a user agent string and dumps the results as a list.
 *       Use the -j flag to print the result as JSON.
 *
 * Thanks to Marcus Bointon (https://github.com/Synchro) for getting this file started
 * and adding the initial JSON parser for a UA string.
 *
 */

// address 5.2 compatibility
if (!defined('__DIR__')) define('__DIR__', dirname(__FILE__));

// address 5.1 compatibility
if (!function_exists('json_decode') || !function_exists('json_encode')) {
	require_once(__DIR__."/lib/json/jsonwrapper.php");
}

// include UAParser.php and make sure to turn off the CLI error
require __DIR__."/UAParser.php";

// deal with timezone issues & logging
if (!ini_get('date.timezone')) {
	date_default_timezone_set(@date_default_timezone_get());
}

/*
 * Gets the latest user agent. Back-ups the old version first. it will fail silently if something is wrong...
 */
function get($silent,$nobackup) {
	if ($data = @file_get_contents("https://raw.github.com/tobie/ua-parser/master/regexes.yaml")) {
		if (file_exists(__DIR__."/resources/regexes.yaml")) {
			if (!$nobackup) { 
				if (!$silent) { print("backing up old YAML file...\n"); }
				if (!copy(__DIR__."/resources/regexes.yaml", __DIR__."/resources/regexes.".date("Ymdhis").".yaml")) {
					if (!$silent) { print("back-up failed...\n"); }
					exit;
				}
			}
		}
		$fp = fopen(__DIR__."/resources/regexes.yaml", "w");
		fwrite($fp, $data);
		fclose($fp);
		if (!$silent) { print("success...\n"); }
	} else {
		if (!$silent) { print("failed to get the file...\n"); }
	}
}

/*
 * Main logic for the CLI for the parser
 */
if (php_sapi_name() == 'cli') {
	
	$args = getopt("gsnl:j:");
	if (isset($args["g"])) {
		$silent   = isset($args["s"]) ? true : false;
		$nobackup = isset($args["n"]) ? true : false;
		if (!$silent) {
			print("getting the YAML file...\n");
		}
		get($silent,$nobackup);
	} else if (isset($args["l"]) && $args["l"]) {
		
		/* Parse the supplied Apache log file */
		
		// load the parser
		$parser = new UA();
		
		// set-up some standard vars
		$i       = 0;
		$output  = "";
		$saved   = array();
		$data    = @fopen($args["l"], "r");
		if ($data) {
			$fp = fopen(__DIR__."/log/results-".date("YmdHis").".txt", "w");
		    while (($line = fgets($data)) !== false) {
				$failure = false;
				$show    = "";
				$line    = str_replace("\n","",$line);
				preg_match("/^(\S+) (\S+) (\S+) \[([^:]+):(\d+:\d+:\d+) ([^\]]+)\] \"(\S+) (.*?) (\S+)\" (\S+) (\S+) (\".*?\") (\"(.*?)\")$/", $line, $items);
				$ua = (isset($items[14])) ? $items[14] : "";
				if (!empty($ua) && ($ua != "-")) {
					$result = $parser->parse($ua);
					if ($result->ua->family == "Other") {
						$output  = "UA Not Found: ".$ua."  [".$line."]\n";
						$show    = "U";
					} else if ($result->os->family == "Other") {
						$output  = "OS Not Found: ".$ua."  [".$line."]\n";
						$show    = "O";
					} else if ($result->device->family == "Generic Smartphone") {
						$output  = "GS:           ".$ua."  [".$line."]\n";
						$show    = "GS";
					} else if ($result->device->family == "Generic Feature Phone") {
						$output  = "GFP:          ".$ua."  [".$line."]\n";
						$show    = "GFP";
					}
					if ((($show == "U") || ($show == "O") || ($show == "GS") || ($show == "GFP")) && !in_array($ua,$saved)) {
						fwrite($fp, $output);
						$saved[] = $ua;
						print $show;
					} else {
						$i = ($i < 20) ? $i+1 : 0;
						if ($i == 0) {
							print ".";
						}
					}
				}
		    }
		    if (!feof($data)) {
		        echo "Error: unexpected fgets() fail\n";
		    }
			fclose($fp);
		    fclose($data);
			print "\ncompleted the evaluation of the log file at ".$args["l"]."\n";
		} else { 
			print "unable to read the file at the supplied path...\n";
		}
	} else if (isset($args["j"]) && $args["j"]) {
		print json_encode(UA::parse($args["j"]));
	} else if (isset($argv[1]) && (($argv[1] != "-j") && ($argv[1] != "-l") && ($argv[1] != "-s") && ($argv[1] != "-n"))) {
		$result = UA::parse($argv[1]);
		print "  ua-parser results for \"".$argv[1]."\"\n";
		foreach ($result as $key=>$value) {
			print "    ".$key.": ".$value."\n";
		}
	} else {
		print "\n";
		print "Usage:\n";
		print "\n";
		print "  php uaparser-cli.php -g [-s] [-n]\n";
		print "    Fetches an updated YAML file for ua-parser and overwrites the current file.\n";
		print "    By default is verbose. Use -s to turn that feature off.\n";
		print "    By default creates a back-up. Use -n to turn that feature off.\n";
		print "\n";
		print "  php uaparser-cli.php -l \"/path/to/apache/logfile\"\n";
		print "    Parses the supplied Apache log file to test UAParser.php\n";
		print "\n";
		print "  php uaparser-cli.php [-j] \"your user agent string\"\n";
		print "    Parses a user agent string and dumps the results as a list.\n";
		print "    Use the -j flag to print the result as JSON.\n";
		print "\n";
	}
} else {
	print "You must run this file from the command line.";
}
