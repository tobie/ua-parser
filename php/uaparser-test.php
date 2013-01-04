<?php

/*!
 * ua-parser-php Test Suite v2.0.0
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
 *            the PHP lib doesn"t support that feature.
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
 * Take the elements from the test cases and test them against the results from uaparser.php
 * @param  object  the result of parsing the supplied test UA
 * @param  string  the expected test case family result
 * @param  string  the expected test case major version result
 * @param  string  the expected test case minor version result
 * @param  string  the expected test case patch version result
 * @param  string  the test case user agent
 * @return string  the result of the test
 */
function testCase($obj,$tc_family,$tc_major,$tc_minor,$tc_patch,$tc_ua) {
    if (!assertEqual($obj->family,$tc_family) || !assertEqual($obj->major,$tc_major) || !assertEqual($obj->minor,$tc_minor) || !assertEqual($obj->patch,$tc_patch)) {
        print "\n    mismatch: got f: ".$obj->family." ma: ".$obj->major." mi: ".$obj->minor." p: ".$obj->patch." and expected f: ".$tc_family." ma: ".$tc_major." mi: ".$tc_minor." p: ".$tc_patch;
        print "\n    the mismatched ua: ".$tc_ua;
        print "\n";
    } else {
        print ".";
    }
}

/*
 * Main logic for the test suite
 */

if (php_sapi_name() == "cli") {
    
    if (!is_dir("../test_resources")) {
        print "ERROR: the full ua-parser project needs to be loaded for the test suite to work. sorry.\n";
        exit;
    }
    
    print "\nrunning uaparser.php against test_user_agent_parser.yaml...\n";
    $data = Spyc::YAMLLoad($basePath."../test_resources/test_user_agent_parser.yaml");
    foreach($data["test_cases"] as $test_case) {
        if (!isset($test_case["js_ua"])) {
            $result = $parser->parse($test_case["user_agent_string"]);
            testCase($result->ua,$test_case["family"],$test_case["major"],$test_case["minor"],$test_case["patch"],$test_case["user_agent_string"]);
        }
    }

    print "\n\nrunning uaparser.php against test_user_agent_parser_os.yaml...\n";
    $data = Spyc::YAMLLoad($basePath."../test_resources/test_user_agent_parser_os.yaml");
    foreach ($data["test_cases"] as $test_case) {
        $result = $parser->parse($test_case["user_agent_string"]);
        testCase($result->os,$test_case["family"],$test_case["major"],$test_case["minor"],$test_case["patch"],$test_case["user_agent_string"]);
    }

    print "\n\nrunning uaparser.php against additional_os_tests.yaml...\n";
    $data = Spyc::YAMLLoad($basePath."../test_resources/additional_os_tests.yaml");
    foreach ($data["test_cases"] as $test_case) {
        $result = $parser->parse($test_case["user_agent_string"]);
        testCase($result->os,$test_case["family"],$test_case["major"],$test_case["minor"],$test_case["patch"],$test_case["user_agent_string"]);
    }

    print "\n\nrunning uaparser.php against test_device.yaml...\n";
    $data = Spyc::YAMLLoad($basePath."../test_resources/test_device.yaml");
    foreach ($data["test_cases"] as $test_case) {
        $result = $parser->parse($test_case["user_agent_string"]);
        if (!assertEqual($result->device->family,$test_case["family"])) {
            print "\n    mismatch: got d: ".$result->device->family." and expected d: ".$test_case["family"];
            print "\n    the mismatched ua: ".$test_case["user_agent_string"];
            print "\n";
        } else {
            print ".";
        }
    }

    print "\n\nrunning uaparser.php against firefox_user_agent_strings.yaml...\n";
    $data = Spyc::YAMLLoad($basePath."../test_resources/firefox_user_agent_strings.yaml");
    foreach ($data["test_cases"] as $test_case) {
        $result = $parser->parse($test_case["user_agent_string"]);
        testCase($result->ua,$test_case["family"],$test_case["major"],$test_case["minor"],$test_case["patch"],$test_case["user_agent_string"]);
    }

    print "\n\ndone testing...\n";
} else {
    print "You must run this file from the command line.";
}
