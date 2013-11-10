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
use UAParser\Util\Fetcher;

/**
 * @group online
 */
class FetcherTest extends AbstractTestCase
{

    /** @var Fetcher */
    private $fetcher;

    public function setUp()
    {
        $this->fetcher = new Fetcher();
    }

    public function testFetch()
    {
        $this->assertInternalType('string', $this->fetcher->fetch());
    }
}
