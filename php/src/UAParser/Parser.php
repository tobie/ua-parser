<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser;

use stdClass;
use UAParser\Exception\FileNotFoundException;
use UAParser\Result\Device;
use UAParser\Result\OperatingSystem;
use UAParser\Result\Client;
use UAParser\Result\UserAgent;

class Parser
{
    /** @var string */
    public static $defaultFile;

    /** @var stdClass */
    protected $regexes;

    /**
     * Start up the parser by importing the json file to $this->regexes
     *
     * @param string $customRegexesFile
     * @throws FileNotFoundException
     */
    public function __construct($customRegexesFile = null)
    {
        $regexesFile = $customRegexesFile !== null ? $customRegexesFile : static::getDefaultFile();
        if (file_exists($regexesFile)) {
            $this->regexes = include $regexesFile;
        } else {
            if ($customRegexesFile !== null) {
                throw FileNotFoundException::customRegexFileNotFound($regexesFile);
            } else {
                throw FileNotFoundException::defaultFileNotFound(static::getDefaultFile());
            }
        }
    }

    /**
     * Sets up some standard variables as well as starts the user agent parsing process
     *
     * @param string $ua a user agent string to test, defaults to an empty string
     * @param array $jsParseBits
     * @return Client
     */
    public function parse($ua, array $jsParseBits = array())
    {
        $result = new Client($ua);

        $result->ua = $this->parseUserAgent($ua, $jsParseBits);
        $result->os = $this->parseOperatingSystem($ua);
        $result->device = $this->parseDevice($ua);

        return $result;
    }

    /**
     * Attempts to see if the user agent matches a user_agents_parsers regex from regexes.php
     *
     * @param string $uaString a user agent string to test
     * @param array $jsParseBits
     * @return UserAgent
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
                $this->regexes['user_agent_parsers'],
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
                $ua->major = isset($regex['v1_replacement']) ? $regex['v1_replacement'] : $matches[2];
                $ua->minor = isset($regex['v2_replacement']) ? $regex['v2_replacement'] : $matches[3];
                $ua->patch = isset($regex['v3_replacement']) ? $regex['v3_replacement'] : $matches[4];
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
     * Attempts to see if the user agent matches an os_parsers regex from regexes.php
     *
     * @param string $uaString a user agent string to test
     * @return OperatingSystem
     */
    private function parseOperatingSystem($uaString)
    {
        $os = new OperatingSystem();

        list($regex, $matches) = $this->matchRegexes(
            $this->regexes['os_parsers'],
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
            $os->family = isset($regex['os_replacement']) ? $regex['os_replacement'] : $matches[1];
            $os->major = isset($regex['os_v1_replacement']) ? $regex['os_v1_replacement'] : $matches[2];
            $os->minor = isset($regex['os_v2_replacement']) ? $regex['os_v2_replacement'] : $matches[3];
            $os->patch = isset($regex['os_v3_replacement']) ? $regex['os_v3_replacement'] : $matches[4];
            $os->patchMinor = isset($regex['os_v4_replacement']) ? $regex['os_v4_replacement'] : $matches[5];
        }

        return $os;
    }

    /**
     * Attempts to see if the user agent matches a device_parsers regex from regexes.php
     * @param string $uaString a user agent string to test
     * @return Device
     */
    private function parseDevice($uaString)
    {
        $device = new Device();

        list($regex, $matches) = $this->matchRegexes($this->regexes['device_parsers'], $uaString, array(1 => 'Other'));
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
            if (preg_match('@' . $regex['regex'] . '@', $subject, $matches)) {
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
    private function replaceString($regex, $key, $string)
    {
        if (!isset($regex[$key])) {
            return $string;
        }

        return str_replace('$1', $string, $regex[$key]);
    }

    private static function getDefaultFile()
    {
        return static::$defaultFile ? static::$defaultFile : __DIR__ . '/../../resources/regexes.php';
    }
}
