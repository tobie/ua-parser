<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2013 Dave Olsen, http://dmolsen.com
 * Copyright (c) 2013-2014 Lars Strojny, http://usrportage.de
 *
 * Released under the MIT license
 */
namespace UAParser;

use UAParser\Exception\FileNotFoundException;
use UAParser\Exception\InvalidArgumentException;
use UAParser\Result\Client;

class Parser extends AbstractParser
{
    /** @var DeviceParser */
    private $deviceParser;

    /** @var OperatingSystemParser */
    private $operatingSystemParser;

    /** @var UserAgentParser */
    private $userAgentParser;

    /**
     * Start up the parser by importing the json file to $this->regexes
     *
     * @param string|array $customRegexesFileOrArray
     * @throws FileNotFoundException
     */
    public function __construct($customRegexesFileOrArray = null)
    {
        if (is_string($customRegexesFileOrArray) || $customRegexesFileOrArray === null) {
            $regexesFile = $customRegexesFileOrArray !== null ? $customRegexesFileOrArray : static::getDefaultFile();
            if (file_exists($regexesFile)) {
                $this->regexes = include $regexesFile;
            } elseif ($customRegexesFileOrArray !== null) {
                throw FileNotFoundException::customRegexFileNotFound($regexesFile);
            } else {
                throw FileNotFoundException::defaultFileNotFound(static::getDefaultFile());
            }

            trigger_error(
                'Using the constructor is deprecated. Use Parser::create(string $file = null) instead',
                E_USER_DEPRECATED
            );

        } elseif (is_array($customRegexesFileOrArray)) {
            $this->regexes = $customRegexesFileOrArray;
        } else {
            throw InvalidArgumentException::unexpectedArgument(
                'array',
                gettype($customRegexesFileOrArray),
                0,
                __METHOD__
            );
        }

        $this->deviceParser = new DeviceParser($this->regexes);
        $this->operatingSystemParser = new OperatingSystemParser($this->regexes);
        $this->userAgentParser = new UserAgentParser($this->regexes);
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
        $client = new Client($userAgent);

        $client->ua = $this->userAgentParser->parseUserAgent($userAgent, $jsParseBits);
        $client->os = $this->operatingSystemParser->parseOperatingSystem($userAgent);
        $client->device = $this->deviceParser->parseDevice($userAgent);

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
            $device->family = $this->multiReplace($regex, 'device_replacement', $matches[1], $matches);
            $device->brand  = $this->multiReplace($regex, 'brand_replacement' , null, $matches);
            $deviceModelDefault = $matches[1] != 'Other' ? $matches[1] : null;
            $device->model  = $this->multiReplace($regex, 'model_replacement' , $deviceModelDefault, $matches);
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
    private function replaceString(array $regex, $key, $string)
    {
        if (!isset($regex[$key])) {
            return $string;
        }

        return str_replace('$1', $string, $regex[$key]);
    }

    /**
     * @param array $regex
     * @param string $key
     * @param string $default
     * @param array $matches
     * @return string
     */
    private function multiReplace(array $regex, $key, $default, array $matches)
    {
        if (!isset($regex[$key])) {
            return $default;
        }
        
        $replacement = preg_replace_callback(
            "|\\$(?<key>\d)|", 
            function ($m) use ($matches){
                return isset($matches[$m['key']]) ? $matches[$m['key']] : "";
            },
            $regex[$key]
        );
        
        return empty($replacement) ? null : $replacement;
    }

    private static function getDefaultFile()
    {
        return static::$defaultFile ? static::$defaultFile : __DIR__ . '/../../resources/regexes.php';
    }
}
