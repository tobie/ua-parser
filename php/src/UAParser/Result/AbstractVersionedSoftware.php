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

abstract class AbstractVersionedSoftware extends AbstractSoftware
{
    /** @return string */
    abstract public function toVersion();

    /** @return string */
    public function toString()
    {
        return join(' ', array_filter(array($this->family, $this->toVersion())));
    }

    /** @return string */
    protected function formatVersion()
    {
        return join('.', array_filter(func_get_args(), 'is_numeric'));
    }
}
