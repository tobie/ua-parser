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

abstract class AbstractSoftware extends AbstractClient
{
    /** @var string */
    public $family = 'Other';

    public function toString()
    {
        return $this->family;
    }
}
