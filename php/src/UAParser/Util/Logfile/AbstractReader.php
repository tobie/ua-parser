<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util\Logfile;

use UAParser\Exception\ReaderException;

abstract class AbstractReader implements ReaderInterface
{
    /** @var ReaderInterface[] */
    private static $readers = array();

    /**
     * @param string $line
     * @return ReaderInterface
     */
    public static function factory($line)
    {
        foreach (static::getReaders() as $reader) {
            if ($reader->test($line)) {
                return $reader;
            }
        }
    }

    private static function getReaders()
    {
        if (static::$readers) {
            return static::$readers;
        }

        static::$readers[] = new ApacheCommonLogFormatReader();

        return static::$readers;
    }

    public function test($line)
    {
        $matches = $this->match($line);

        return isset($matches['userAgentString']);
    }

    public function read($line)
    {
        $matches = $this->match($line);

        if (!isset($matches['userAgentString'])) {
            throw ReaderException::userAgentParserError($line);
        }

        return $matches['userAgentString'];
    }

    protected function match($line)
    {
        if (preg_match($this->getRegex(), $line, $matches)) {
            return $matches;
        }

        return array();
    }

    abstract protected function getRegex();
}
