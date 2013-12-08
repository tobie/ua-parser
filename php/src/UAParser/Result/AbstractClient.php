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
