<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Result;

class OperatingSystem extends AbstractVersionedSoftware
{
    /** @var string */
    public $major;

    /** @var string */
    public $minor;

    /** @var string */
    public $patch;

    /** @var string */
    public $patchMinor;

    public function toVersion()
    {
        return $this->formatVersion($this->major, $this->minor, $this->patch, $this->patchMinor);
    }
}
