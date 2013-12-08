<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2013 Dave Olsen, http://dmolsen.com
 * Copyright (c) 2013-2014 Lars Strojny, http://usrportage.de
 *
 * Released under the MIT license
 */
namespace UAParser\Result;

class Client extends AbstractClient
{
    /** @var UserAgent */
    public $ua;

    /** @var OperatingSystem */
    public $os;

    /** @var Device */
    public $device;

    /** @var string */
    public $originalUserAgent;

    /**
     * @param string $originalUserAgent
     */
    public function __construct($originalUserAgent)
    {
        $this->originalUserAgent = $originalUserAgent;
    }

    public function toString()
    {
        return $this->ua->toString() . '/' . $this->os->toString();
    }
}
