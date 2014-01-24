<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests\Result;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use UAParser\Result\OperatingSystem;

class OperatingSystemTest extends AbstractTestCase
{
    /** @var OperatingSystem */
    private $operatingSystem;

    public function setUp()
    {
        $this->operatingSystem = new OperatingSystem();
    }

    public function testBugWith0InVersion()
    {
        $this->operatingSystem->major = 0;
        $this->operatingSystem->minor = 0;
        $this->operatingSystem->patch = 0;
        $this->operatingSystem->patchMinor = 0;

        $this->assertSame('0.0.0.0', $this->operatingSystem->toVersion());
        $this->assertSame('Other 0.0.0.0', $this->operatingSystem->toString());
    }
}
