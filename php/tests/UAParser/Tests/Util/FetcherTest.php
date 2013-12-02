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
    public function testFetchSuccess()
    {
        $fetcher = new Fetcher();
        $this->assertInternalType('string', $fetcher->fetch());
    }

    public function testFetchError()
    {
        $fetcher = new Fetcher(
            stream_context_create(
                array(
                    'ssl' => array(
                        'verify_peer' => true,
                        'CN_match'    => 'invalid.com',
                    )
                )
            )
        );

        $this->setExpectedException(
            'UAParser\Exception\FetcherException',
            'Could not fetch HTTP resource "https://raw.github.com/tobie/ua-parser/master/regexes.yaml": file_get_contents(https://raw.github.com/tobie/ua-parser/master/regexes.yaml): failed to open stream: operation failed'
        );

        $fetcher->fetch();
    }
}
