<?php
/**
 * ua-parser-php v2.1.1
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
 * Services_JSON, for loading the JSON in sub-PHP 5.2 installs, is licensed under the MIT license
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
 *   - Ryan Parman (https://github.com/skyzyx)
 *   - Pravin Dahal (https://github.com/pravindahal)
 */
namespace UAParser;

use UAParser\Exception\FileNotFoundException;
use UAParser\Result\Device;
use UAParser\Result\OperatingSystem;
use UAParser\Result\Result;
use UAParser\Result\UserAgent;

class Parser
{
    protected $regexes;
    protected $log = false;

    /**
     * Start up the parser by importing the json file to $this->regexes
     */
    public function __construct($customRegexesFile = null)
    {
        $regexesFile = ($customRegexesFile !== null) ? $customRegexesFile : __DIR__ . '/../../resources/regexes.json';
        if (file_exists($regexesFile)) {
            $this->regexes = json_decode(file_get_contents($regexesFile));
        } else {
            if ($customRegexesFile !== null) {
                $message = 'ua-parser can\'t find the custom regexes file you supplied (' . $customRegexesFile . '). Please make sure you have the correct path.';
            } else {
                $message = 'Please download the regexes.json file before using uaparser.php.';
                if (php_sapi_name() == 'cli') {
                    $message .= ' (php uaparser-cli.php -g)';
                }
            }

            throw new FileNotFoundException($message);
        }
    }

    /**
     * Sets up some standard variables as well as starts the user agent parsing process
     * @param  string $ua a user agent string to test, defaults to an empty string
     * @param  array $jsParseBits
     * @return object the result of the user agent parsing
     */
    public function parse($ua, array $jsParseBits = array())
    {
        $result = new Result($ua);

        // figure out the ua, os, and device properties if possible
        $result->ua = $this->parseUserAgent($ua, $jsParseBits);
        $result->os = $this->parseOperatingSystem($ua);
        $result->device = $this->parseDevice($ua);

        // log the results when testing
        if ($this->log) {
            $this->log($result);
        }

        return $result;
    }

    /**
     * Attempts to see if the user agent matches a user_agents_parsers regex from regexes.json
     * @param  string $uaString a user agent string to test
     * @param  array $jsParseBits
     * @return object the result of the user agent parsing
     */
    private function parseUserAgent($uaString, array $jsParseBits = array())
    {
        $ua = new UserAgent();

        if (isset($jsParseBits['js_user_agent_family']) && $jsParseBits['js_user_agent_family']) {

            $ua->family = $jsParseBits['js_user_agent_family'];
            $ua->major  = $jsParseBits['js_user_agent_v1'];
            $ua->minor  = $jsParseBits['js_user_agent_v2'];
            $ua->patch  = $jsParseBits['js_user_agent_v3'];

        } else {

            // run the regexes to match things up
            $uaRegexes = $this->regexes->user_agent_parsers;
            foreach ($uaRegexes as $uaRegex) {

                // tests the supplied regex against the user agent
                if (preg_match('/' . str_replace('/', '\/', str_replace('\/', '/', $uaRegex->regex)) . '/', $uaString, $matches)) {

                    // Make sure matches are at least set to null or Other
                    if (!isset($matches[1])) {
                        $matches[1] = 'Other';
                    }
                    if (!isset($matches[2])) {
                        $matches[2] = null;
                    }
                    if (!isset($matches[3])) {
                        $matches[3] = null;
                    }
                    if (!isset($matches[4])) {
                        $matches[4] = null;
                    }

                    // ua name
                    $ua->family = isset($uaRegex->family_replacement) ? str_replace('$1', $matches[1], $uaRegex->family_replacement) : $matches[1];

                    // version properties
                    $ua->major = isset($uaRegex->v1_replacement) ? $uaRegex->v1_replacement : $matches[2];
                    $ua->minor = isset($uaRegex->v2_replacement) ? $uaRegex->v2_replacement : $matches[3];
                    $ua->patch = isset($uaRegex->v3_replacement) ? $uaRegex->v3_replacement : $matches[4];

                    break;
                }

            }
        }

        if (isset($jsParseBits['js_user_agent_string'])) {

            $jsUserAgentString = $jsParseBits['js_user_agent_string'];
            if (strpos($jsUserAgentString, 'Chrome/') !== false && strpos($uaString, 'chromeframe') !== false) {

                $override   = $this->parseUserAgent($jsUserAgentString);
                $ua->family = sprintf('Chrome Frame (%s %s)', $ua->family, $ua->major);
                $ua->major  = $override->major;
                $ua->minor  = $override->minor;
                $ua->patch  = $override->patch;
            }
        }

        return $ua;
    }

    /**
     * Attempts to see if the user agent matches an os_parsers regex from regexes.json
     * @param  string $uaString a user agent string to test
     * @return object the result of the os parsing
     */
    private function parseOperatingSystem($uaString)
    {
        $os = new OperatingSystem();

        // run the regexes to match things up
        $osRegexes = $this->regexes->os_parsers;
        foreach ($osRegexes as $osRegex) {

            if (preg_match('/' . str_replace('/', '\/', str_replace('\/', '/', $osRegex->regex)) . '/', $uaString, $matches)) {

                // Make sure matches are at least set to null or Other
                if (!isset($matches[1])) {
                    $matches[1] = 'Other';
                }
                if (!isset($matches[2])) {
                    $matches[2] = null;
                }
                if (!isset($matches[3])) {
                    $matches[3] = null;
                }
                if (!isset($matches[4])) {
                    $matches[4] = null;
                }
                if (!isset($matches[5])) {
                    $matches[5] = null;
                }

                // os name
                $os->family = isset($osRegex->os_replacement) ? $osRegex->os_replacement : $matches[1];

                // version properties
                $os->major = isset($osRegex->os_v1_replacement) ? $osRegex->os_v1_replacement : $matches[2];
                $os->minor = isset($osRegex->os_v2_replacement) ? $osRegex->os_v2_replacement : $matches[3];
                $os->patch = isset($osRegex->os_v3_replacement) ? $osRegex->os_v3_replacement : $matches[4];
                $os->patchMinor = isset($osRegex->os_v4_replacement) ? $osRegex->os_v4_replacement : $matches[5];

                return $os;
            }

        }

        return $os;

    }

    /**
     * Attempts to see if the user agent matches a device_parsers regex from regexes.json
     * @param  string $uaString a user agent string to test
     * @return object the result of the device parsing
     */
    private function parseDevice($uaString)
    {
        // build the default obj that will be returned
        $device = new Device();

        // run the regexes to match things up
        $deviceRegexes = $this->regexes->device_parsers;
        foreach ($deviceRegexes as $deviceRegex) {

            if (preg_match('/' . str_replace('/', '\/', str_replace('\/', '/', $deviceRegex->regex)) . '/', $uaString, $matches)) {

                // Make sure matches are at least set to null or Other
                if (!isset($matches[1])) {
                    $matches[1] = 'Other';
                }

                // device name
                $device->family = isset($deviceRegex->device_replacement) ? str_replace('$1', $matches[1], $deviceRegex->device_replacement) : $matches[1];

                break;

            }

        }

        return $device;
    }

    /**
     * Logs the user agent info
     */
    protected function log($data)
    {
        $jsonData = json_encode($data);
        $fp       = fopen(dirname(__FILE__) . DIRECTORY_SEPARATOR . 'log/user_agents.log', 'a');
        fwrite($fp, $jsonData . "\r\n");
        fclose($fp);
    }
}
