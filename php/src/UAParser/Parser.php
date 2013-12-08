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
        } elseif ($customRegexesFile !== null) {
            throw FileNotFoundException::customRegexFileNotFound($regexesFile);
        } else {
            throw FileNotFoundException::defaultFileNotFound($regexesFile);
        }
    }

    /**
     * Sets up some standard variables as well as starts the user agent parsing process
     *
     * @param string $userAgent a user agent string to test, defaults to an empty string
     * @param array $jsParseBits
     * @return Client
     */
    public function parse($userAgent, array $jsParseBits = array())
    {
        $result = new Client($userAgent);

        $result->ua = $this->parseUserAgent($userAgent, $jsParseBits);
        $result->os = $this->parseOperatingSystem($userAgent);
        $result->device = $this->parseDevice($userAgent);

        return $result;
    }

    /**
     * Attempts to see if the user agent matches a user_agents_parsers regex from regexes.php
     *
     * @param string $userAgent a user agent string to test
     * @param array $jsParseBits
     * @return UserAgent
     */
    private function parseUserAgent($userAgent, array $jsParseBits = array())
    {
        $ua = new UserAgent();

        if (isset($jsParseBits['js_user_agent_family']) && $jsParseBits['js_user_agent_family']) {

            $ua->family = $jsParseBits['js_user_agent_family'];
            $ua->major = $jsParseBits['js_user_agent_v1'];
            $ua->minor = $jsParseBits['js_user_agent_v2'];
            $ua->patch = $jsParseBits['js_user_agent_v3'];

        } else {

            list($regex, $matches) = $this->tryMatch($this->regexes['user_agent_parsers'], $userAgent);

            if ($matches) {
                $ua->family = $this->replaceString($regex, 'family_replacement', $matches[1]);
                $ua->major = $this->replaceString($regex, 'v1_replacement', $matches[2]);
                $ua->minor = $this->replaceString($regex, 'v2_replacement', $matches[3]);
                $ua->patch = $this->replaceString($regex, 'v3_replacement', $matches[4]);
            }
        }

        if (isset($jsParseBits['js_user_agent_string'])) {
            $jsUserAgentString = $jsParseBits['js_user_agent_string'];
            if (strpos($jsUserAgentString, 'Chrome/') !== false && strpos($userAgent, 'chromeframe') !== false) {
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
     * @param string $userAgent a user agent string to test
     * @return OperatingSystem
     */
    private function parseOperatingSystem($userAgent)
    {
        $os = new OperatingSystem();

        list($regex, $matches) = $this->tryMatch($this->regexes['os_parsers'], $userAgent);

        if ($matches) {
            $os->family = $this->replaceString($regex, 'os_replacement', $matches[1]);
            $os->major = $this->replaceString($regex, 'os_v1_replacement', $matches[2]);
            $os->minor = $this->replaceString($regex, 'os_v2_replacement', $matches[3]);
            $os->patch = $this->replaceString($regex, 'os_v3_replacement', $matches[4]);
            $os->patchMinor = $this->replaceString($regex, 'os_v4_replacement', $matches[5]);
        }

        return $os;
    }

    /**
     * Attempts to see if the user agent matches a device_parsers regex from regexes.php
     * @param string $userAgent a user agent string to test
     * @return Device
     */
    private function parseDevice($userAgent)
    {
        $device = new Device();

        list($regex, $matches) = $this->tryMatch($this->regexes['device_parsers'], $userAgent);

        if ($matches) {
            $device->family = $this->replaceString($regex, 'device_replacement', $matches[1]);
        }

        return $device;
    }

    /**
     * @param array $regexes
     * @param string $userAgent
     * @return array
     */
    private function tryMatch(array $regexes, $userAgent)
    {

        foreach ($regexes as $regex) {
            if (preg_match('@' . $regex['regex'] . '@', $userAgent, $matches)) {

                $defaults = array(
                    1 => 'Other',
                    2 => null,
                    3 => null,
                    4 => null,
                    5 => null,
                );

                return array($regex, $matches + $defaults);
            }
        }

        return array(null, null);
    }

    /**
     * @param array $regex
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
