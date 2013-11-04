<?php

/*!
 * ua-parser-php Test Suite v2.1.1
 *
 * Copyright (c) 2012 Dave Olsen, http://dmolsen.com
 * Licensed under the MIT license
 *
 * spyc-0.5, for loading the YAML, is licensed under the MIT license.
 *
 * This is the test suit for ua-parser-php to make sure it matches
 * the standards set forth for ua-parser libraries.
 *
 * IMPORTANT: This test suite skips the Chrome Frame tests because
 *            the PHP lib doesn't support that feature.
 *
 */

// define the base path for the file
$basePath = dirname(__FILE__).DIRECTORY_SEPARATOR;

// include the YAML library
require_once($basePath."lib/spyc-0.5/spyc.php");

// include UAParser.php
require_once($basePath."uaparser.php");

// set-up the parser
$parser = new UAParser;

/**
 * assert that the actual result and the expected result match
 * NOTE: the lib calls for certain attributes to be set to null but the test cases are empty strings
 * @param  any      the actual result of the parsing
 * @param  any      the expected result of the test
 * @return boolean  the result of the test
 */
function assertEqual($actual,$expected) {
	$actual = ($actual == null) ? '' : $actual;
	$result = ($actual === $expected) ? true : false;
	return $result;
}

/**
 * reports the mismatch between a test and what was returned from uaparser.php
 * @param  object  the result of parsing the supplied test UA
 * @param  array  the test case properties
 * @param  string  the name of the test file
 * @return string  error report for the failing test
 */
function reportMismatch($obj,$tc,$tf) {
	if ($tf == "test_device") {
		$info = "mismatch: got d: ".$obj->device->family." and expected d: ".$tc['family'];
	} else if (($tf == "test_user_agent_parser_os") || ($tf == "additional_os_tests")) {
		$info = "mismatch: got f: ".$obj->os->family." ma: ".$obj->os->major." mi: ".$obj->os->minor." p: ".$obj->os->patch." pm: ".$obj->os->patch_minor."  expected f: ".$tc['family']." ma: ".$tc['major']." mi: ".$tc['minor']." p: ".$tc['patch']." pm: ".$tc['patch_minor'];
	} else {
		$info = "mismatch: got f: ".$obj->ua->family." ma: ".$obj->ua->major." mi: ".$obj->ua->minor." p: ".$obj->ua->patch." expected f: ".$tc['family']." ma: ".$tc['major']." mi: ".$tc['minor']." p: ".$tc['patch'];
	}
	print "\n    ".$info;
	print "\n    the mismatched ua: ".$tc['user_agent_string'];
	print "\n";
}

/*
 * Main logic for the test suite
 */

if (php_sapi_name() == "cli") {
    
    if (!is_dir("../test_resources")) {
        print "ERROR: the full ua-parser project needs to be loaded for the test suite to work. sorry.\n";
        exit;
    }
    
	// test files and properties normally in them
	$test_files = array("test_user_agent_parser","test_user_agent_parser_os","additional_os_tests","test_device","firefox_user_agent_strings");
	$test_props = array("family", "major", "minor", "patch", "patch_minor");
	$test_types = array("test_device" => "device", "test_user_agent_parser_os" => "os", "additional_os_tests" => "os", "test_user_agent_parser" => "ua", "firefox_user_agent_strings" => "ua");

	foreach($test_files as $test_file) {
		print "\n\nrunning uaparser.php against ".$test_file.".yaml...\n";
	    $data = Spyc::YAMLLoad($basePath."../test_resources/".$test_file.".yaml");
		foreach($data["test_cases"] as $test_case) {
	        if (!isset($test_case["js_ua"])) {
				$bool   = true;
	            $result = $parser->parse($test_case["user_agent_string"]);
				foreach ($test_props as $test_prop) {
					if ($bool) {
						$bool = isset($test_case[$test_prop]) ? assertEqual($result->$test_types[$test_file]->$test_prop,$test_case[$test_prop]) : $bool;
					}
				}
				if (!$bool) {
					reportMismatch($result,$test_case,$test_file);
				} else {
					print ".";
				}
	        }
	    }
	}

    print "\n\ndone testing...\n";
} else {
    print "You must run this file from the command line.";
}
