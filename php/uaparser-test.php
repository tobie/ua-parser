<?php

/*!
 * ua-parser-php Test Suite v1.0.0
 *
 * Copyright (c) 2012 Dave Olsen, http://dmolsen.com
 * Licensed under the MIT license
 *
 * This is the test suit for ua-parser-php to make sure it matches
 * the standards set forth for ua-parser libraries.
 *
 * IMPORTANT: This test suite skips the Chrome Frame tests because
 *            this lib doesn't support that feature. It also skips
 *            the Blackberry Playbook test in test_device.yaml
 *            because my lib considers it a mobile device. The test
 *            suite apparently doesn't.
 *
 */

// address 5.2 compatibility
if (!defined('__DIR__')) define('__DIR__', dirname(__FILE__));

// include UAParser.php and make sure to turn off the CLI error
require __DIR__."/UAParser.php";

/**
 * Take the elements from the test and test the against the results from UAParser.php
 *
 * @return string the result of the test
 */
function test($tc_ua,$tc_family,$tc_major,$tc_minor,$tc_patch,$type) {
	$ua = UA::parse($tc_ua);
	if ($type == "b") {
		$family_result = ($ua->family == $tc_family) ? true : false;
		$major_result  = ($ua->major == $tc_major) ? true : false;
		$minor_result  = ($ua->minor == $tc_minor) ? true : false;
		$patch_result  = ($ua->patch == $tc_patch) ? true : false;
	} else if ($type == "os") {
		$family_result = ($ua->os == $tc_family) ? true : false;
		$major_result  = ($ua->osMajor == $tc_major) ? true : false;
		$minor_result  = ($ua->osMinor == $tc_minor) ? true : false;
		$patch_result  = ($ua->osPatch == $tc_patch) ? true : false;
	}
	
	if (!$family_result || !$major_result || !$minor_result || !$patch_result) {
		print "\n    mismatch: got ".$ua->family." ".$ua->major." ".$ua->minor." ".$ua->patch." and expected ".$tc_family." ".$tc_major." ".$tc_minor." ".$tc_patch;
		print "\n    the mismatched ua: ".$tc_ua;
	} else {
		print ".";
	}
}

/*
 * Main logic for the test suite
 */

if (php_sapi_name() == 'cli') {
	
	print "\nrunning UAParser.php against test_user_agent_parser.yaml...\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/test_user_agent_parser.yaml");
	foreach($data['test_cases'] as $test_case) {
		if (!isset($test_case['js_ua'])) {
			test($test_case['user_agent_string'],$test_case['family'],$test_case['major'],$test_case['minor'],$test_case['patch'],"b");
		}
	}

	print "\n\nrunning UAParser.php against test_user_agent_parser_os.yaml...\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/test_user_agent_parser_os.yaml");
	foreach ($data['test_cases'] as $test_case) {
		test($test_case['user_agent_string'],$test_case['family'],$test_case['major'],$test_case['minor'],$test_case['patch'],"os");
	}

	print "\n\nrunning UAParser.php against additional_os_tests.yaml...\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/additional_os_tests.yaml");
	foreach ($data['test_cases'] as $test_case) {
		test($test_case['user_agent_string'],$test_case['family'],$test_case['major'],$test_case['minor'],$test_case['patch'],"os");
	}

	print "\n\nrunning UAParser.php against test_device.yaml...\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/test_device.yaml");
	foreach ($data['test_cases'] as $test_case) {
		if ($test_case['family'] != "Blackberry Playbook") {
			$ua = UA::parse($test_case['user_agent_string']);
			$family_result = ($ua->device == $test_case['family']) ? true : false;
			$mobile_result = ($ua->isMobileDevice == $test_case['is_mobile']) ? true : false;
			$spider_result = ($ua->isSpider == $test_case['is_spider']) ? true : false;

			if (!$family_result || !$mobile_result || !$spider_result) {
				print "\n    mismatch: got d: ".$ua->device." m: ".$ua->isMobile." s: ".$ua->isSpider." and expected d: ".$test_case['family']." m: ".$test_case['is_mobile']." s: ".$test_case['is_spider'];
				print "\n    the mismatched ua: ".$test_case['user_agent_string'];
			} else {
				print ".";
			}
		}
	}

	print "\n\nrunning UAParser.php against firefox_user_agent_strings.yaml...\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/firefox_user_agent_strings.yaml");
	foreach ($data['test_cases'] as $test_case) {
		test($test_case['user_agent_string'],$test_case['family'],$test_case['major'],$test_case['minor'],$test_case['patch'],"b");
	}


	print "\n\nrunning UAParser.php against pgts_browser_list.yaml... (takes a long time to load)\n";
	$data = Spyc::YAMLLoad(__DIR__."/../test_resources/pgts_browser_list.yaml");
	foreach ($data['test_cases'] as $test_case) {
		test($test_case['user_agent_string'],$test_case['family'],$test_case['major'],$test_case['minor'],$test_case['patch'],"b");
	}

	print "\ndone testing...\n";
} else {
	print "You must run this file from the command line.";
}
