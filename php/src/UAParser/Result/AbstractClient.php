<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Result;

abstract class AbstractClient
{
    /** @return string */
    abstract public function toString();

    /** @return string */
    public function __toString()
    {
        return $this->toString();
    }
}
