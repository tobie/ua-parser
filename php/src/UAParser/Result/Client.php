<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
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
    public $uaOriginal;

    /**
     * @param string $uaOriginal
     */
    public function __construct($uaOriginal)
    {
        $this->uaOriginal = $uaOriginal;
    }

    public function toString()
    {
        return $this->ua->toString() . '/' . $this->os->toString();
    }
}
