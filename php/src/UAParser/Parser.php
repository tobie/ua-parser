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

use stdClass;
use UAParser\Exception\FileNotFoundException;
use UAParser\Result\Device;
use UAParser\Result\OperatingSystem;
use UAParser\Result\Result;
use UAParser\Result\UserAgent;

class Parser
{
    protected $regexes;

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
     *
     * @param string $ua a user agent string to test, defaults to an empty string
     * @param array $jsParseBits
     * @return object the result of the user agent parsing
     */
    public function parse($ua, array $jsParseBits = array())
    {
        $result = new Result($ua);

        $result->ua = $this->parseUserAgent($ua, $jsParseBits);
        $result->os = $this->parseOperatingSystem($ua);
        $result->device = $this->parseDevice($ua);

        return $result;
    }

    /**
     * Attempts to see if the user agent matches a user_agents_parsers regex from regexes.json
     *
     * @param string $uaString a user agent string to test
     * @param array $jsParseBits
     * @return object the result of the user agent parsing
     */
    private function parseUserAgent($uaString, array $jsParseBits = array())
    {
        $ua = new UserAgent();

        if (isset($jsParseBits['js_user_agent_family']) && $jsParseBits['js_user_agent_family']) {
            $ua->family = $jsParseBits['js_user_agent_family'];
            $ua->major = $jsParseBits['js_user_agent_v1'];
            $ua->minor = $jsParseBits['js_user_agent_v2'];
            $ua->patch = $jsParseBits['js_user_agent_v3'];
        } else {
            list($regex, $matches) = $this->matchRegexes(
                $this->regexes->user_agent_parsers,
                $uaString,
                array(
                    1 => 'Other',
                    2 => null,
                    3 => null,
                    4 => null,
                )
            );

            if ($matches) {
                $ua->family = $this->replaceString($regex, 'family_replacement', $matches[1]);
                $ua->major = isset($regex->v1_replacement) ? $regex->v1_replacement : $matches[2];
                $ua->minor = isset($regex->v2_replacement) ? $regex->v2_replacement : $matches[3];
                $ua->patch = isset($regex->v3_replacement) ? $regex->v3_replacement : $matches[4];
            }
        }

        if (isset($jsParseBits['js_user_agent_string'])) {
            $jsUserAgentString = $jsParseBits['js_user_agent_string'];
            if (strpos($jsUserAgentString, 'Chrome/') !== false && strpos($uaString, 'chromeframe') !== false) {
                $override = $this->parseUserAgent($jsUserAgentString);
                $ua->family = sprintf('Chrome Frame (%s %s)', $ua->family, $ua->major);
                $ua->major = $override->major;
                $ua->minor = $override->minor;
                $ua->patch = $override->patch;
            }
        }

        return $ua;
    }

    /**
     * Attempts to see if the user agent matches an os_parsers regex from regexes.json
     *
     * @param string $uaString a user agent string to test
     * @return object the result of the os parsing
     */
    private function parseOperatingSystem($uaString)
    {
        $os = new OperatingSystem();

        list($regex, $matches) = $this->matchRegexes(
            $this->regexes->os_parsers,
            $uaString,
            array(
                1 => 'Other',
                2 => null,
                3 => null,
                4 => null,
                5 => null,
            )
        );

        if ($matches) {
            $os->family = isset($regex->os_replacement) ? $regex->os_replacement : $matches[1];
            $os->major = isset($regex->os_v1_replacement) ? $regex->os_v1_replacement : $matches[2];
            $os->minor = isset($regex->os_v2_replacement) ? $regex->os_v2_replacement : $matches[3];
            $os->patch = isset($regex->os_v3_replacement) ? $regex->os_v3_replacement : $matches[4];
            $os->patchMinor = isset($regex->os_v4_replacement) ? $regex->os_v4_replacement : $matches[5];
        }

        return $os;
    }

    /**
     * Attempts to see if the user agent matches a device_parsers regex from regexes.json
     * @param string $uaString a user agent string to test
     * @return object the result of the device parsing
     */
    private function parseDevice($uaString)
    {
        $device = new Device();

        list($regex, $matches) = $this->matchRegexes($this->regexes->device_parsers, $uaString, array(1 => 'Other'));
        if ($matches) {
            $device->family = $this->replaceString($regex, 'device_replacement', $matches[1]);
        }

        return $device;
    }

    /**
     * @param array $regexes
     * @param string $subject
     * @param array $defaults
     * @return array
     */
    private function matchRegexes(array $regexes, $subject, array $defaults = array())
    {
        foreach ($regexes as $regex) {
            if (preg_match('@' . $regex->regex . '@', $subject, $matches)) {
                return array(
                    $regex,
                    $matches + $defaults
                );
            }
        }

        return array(null, null);
    }

    /**
     * @param stdClass $regex
     * @param string $key
     * @param string $string
     * @return string
     */
    private function replaceString(stdClass $regex, $key, $string)
    {
        if (!isset($regex->{$key})) {
            return $string;
        }

        return str_replace('$1', $string, $regex->{$key});
    }
}
