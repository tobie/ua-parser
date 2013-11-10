<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests\Util\Logfile;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use UAParser\Util\Logfile\ReaderInterface;

abstract class AbstractReaderTest extends AbstractTestCase
{
    /** @var ReaderInterface */
    protected $reader;

    /** @var string */
    protected $line;

    /** @var string */
    protected $userAgentString;

    /** @var string */
    protected $exampleLogFile;

    public function testTestEmptyLine()
    {
        $this->assertFalse($this->reader->test(''));
    }

    public function testTestRealLine()
    {
        $this->assertTrue($this->reader->test($this->line));
    }

    public function testReadEmptyLine()
    {
        $this->setExpectedException(
            'UAParser\Exception\ReaderException',
            'Cannot extract user agent string from line "invalid"'
        );
        $this->reader->read('invalid');
    }

    public function testReadRealLine()
    {
        $this->assertSame($this->userAgentString, $this->reader->read($this->line));
    }

    public function testReadingFile()
    {
        if (!$this->exampleLogFile) {
            $this->markTestSkipped('Try with your own log file');
        }

        foreach (file($this->exampleLogFile) as $line) {
            $this->assertTrue($this->reader->test($line));
            $this->assertGreaterThanOrEqual(1, strlen($this->reader->read($line)));
        }
    }
}
