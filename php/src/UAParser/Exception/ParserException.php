<?php
namespace UAParser\Exception;

class ParserException extends DomainException
{
    public static function userAgentParserError($line)
    {
        return new static(sprintf('Cannot extract user agent string from line "%s"', $line));
    }
}
