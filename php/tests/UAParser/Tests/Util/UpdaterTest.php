<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests\Util;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use UAParser\Util\Converter;
use UAParser\Util\Updater;
use PHPUnit_Framework_MockObject_MockObject as MockObject;

/**
 * @group online
 */
class UpdaterTest extends AbstractTestCase
{
    /** @var Converter|MockObject */
    private $converter;

    /** @var Updater */
    private $updater;

    public function setUp()
    {
        $this->converter = $this
            ->getMockBuilder('UAParser\Util\Converter')
            ->disableOriginalConstructor()
            ->disableOriginalClone()
            ->getMock();
        $this->updater = new Updater($this->converter);
    }

    public function testUpdate()
    {
        $this->converter
            ->expects($this->once())
            ->method('convertString')
            ->with($this->stringContains('user_agent_parsers:'));

        $this->updater->update();
    }
}
