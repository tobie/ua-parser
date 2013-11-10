<?php
namespace UAParser\Util\Logfile;

interface ReaderInterface
{
    /**
     * @param string $line
     * @return bool
     */
    public function test($line);

    /**
     * @param string $line
     * @return string
     */
    public function read($line);
}
