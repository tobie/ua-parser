<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use Symfony\Component\Finder\Finder;
use Symfony\Component\Finder\SplFileInfo;
use Symfony\Component\Yaml\Yaml;
use UAParser\AbstractParser;
use UAParser\Parser;

abstract class AbstractParserTest extends AbstractTestCase
{
    public function testCreateDefault()
    {
        $parserClassName = $this->getParserClassName();

        $this->assertInstanceOf($parserClassName, $parserClassName::create());
    }

    public function testCreateCustom()
    {
        $parserClassName = $this->getParserClassName();

        $this->assertInstanceOf(
            $parserClassName,
            $parserClassName::create(__DIR__ . '/../../../resources/regexes.php')
        );
    }

    public function testCreateCustomWithInvalidFile()
    {
        $parserClassName = $this->getParserClassName();

        $this->setExpectedException(
            'UAParser\Exception\FileNotFoundException',
            'ua-parser cannot find the custom regexes file you supplied ("foo.php"). Please make sure you have the correct path.'
        );
        $parserClassName::create('foo.php');
    }

    /** @deprecated */
    public function testExceptionOnFileNotFoundInvalidDefault()
    {
        $parserClassName = $this->getParserClassName();

        $this->setExpectedException(
            'UAParser\Exception\FileNotFoundException',
            'Please download the "invalidFile" file before using ua-parser by running "php bin/uaparser.php uaparser:update"'
        );

        $parserClassName::$defaultFile = 'invalidFile';
        $parserClassName::create();
    }

    public function tearDown()
    {
        AbstractParser::$defaultFile = null;
    }

    abstract protected function getParserClassName();
}
