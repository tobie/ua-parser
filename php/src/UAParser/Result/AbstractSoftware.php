<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Result;

abstract class AbstractSoftware extends AbstractClient
{
    /** @var string */
    public $family = 'Other';

    public function toString()
    {
        return $this->family;
    }
}
