<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util\Logfile;

use UAParser\Exception\ParserException;

abstract class AbstractReader implements ReaderInterface
{
    /**
     * @param string $line
     * @return ReaderInterface
     */
    public static function factory($line)
    {
        $classes = array(
            'UAParser\Util\Logfile\ApacheCommonLogFormatReader'
        );

        foreach ($classes as $class) {
            /** @var ReaderInterface $reader */
            $reader = new $class();
            if ($reader->test($line)) {
                return $reader;
            }
        }
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
            throw ParserException::userAgentParserError($line);
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
