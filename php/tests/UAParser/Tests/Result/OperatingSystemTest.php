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
use UAParser\Result\UserAgent;

class UserAgentTest extends AbstractTestCase
{
    /** @var UserAgent */
    private $userAgent;

    public function setUp()
    {
        $this->userAgent = new UserAgent();
    }

    public function testBugWith0InVersion()
    {
        $this->userAgent->major = 0;
        $this->userAgent->minor = 0;
        $this->userAgent->patch = 0;

        $this->assertSame('0.0.0', $this->userAgent->toVersion());
        $this->assertSame('Other 0.0.0', $this->userAgent->toString());
    }
}
