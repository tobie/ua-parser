<?php
namespace UAParser\Util\Logfile;

use UAParser\Exception\ParserException;

abstract class AbstractReader implements ReaderInterface
{
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
