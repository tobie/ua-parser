<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Exception;

class ReaderException extends DomainException
{
    public static function userAgentParserError($line)
    {
        return new static(sprintf('Cannot extract user agent string from line "%s"', $line));
    }
}
