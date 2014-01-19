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
     * @param array $regexes
     */
    public function __construct(array $regexes)
    {
        parent::__construct($regexes);
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

        return $client;
    }
}
