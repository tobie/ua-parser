<?php
namespace UAParser\Result;

class Result extends AbstractResult
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
