<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests;

use Symfony\Component\Finder\Finder;
use Symfony\Component\Finder\SplFileInfo;
use Symfony\Component\Yaml\Yaml;
use UAParser\Parser;

class ParserTest extends AbstractParserTest
{
    /** @var Parser */
    private $parser;

    /** @var Parser */
    private static $staticParser;

    public static function setUpBeforeClass()
    {
        static::$staticParser = Parser::create();
    }

    public function setUp()
    {
        $this->parser = static::$staticParser;
    }

    public static function getOsTestData()
    {
        $resources = Finder::create()
            ->files()
            ->name('test_device.yaml')
            ->name('*os*.yaml')
            ->notName('test_device.yaml')
            ->notName('pgts_browser_list-orig.yaml');

        return static::createTestData($resources);
    }

    public static function getUserAgentTestData()
    {
        $resources = Finder::create()
            ->files()
            ->name('*.yaml')
            ->notName('*os*')
            ->notName('test_device.yaml')
            ->notName('pgts_browser_list-orig.yaml');

        return static::createTestData($resources);
    }

    public static function getDeviceTestData()
    {
        $resources = Finder::create()
            ->files()
            ->name('test_device.yaml');

        return static::createTestData($resources);
    }

    /** @dataProvider getDeviceTestData */
    public function testDeviceParsing($userAgent, array $jsUserAgent, $family)
    {
        $result = $this->parser->parse($userAgent, $jsUserAgent);

        $this->assertSame($family, $result->device->family);
    }

    /** @dataProvider getOsTestData */
    public function testOperatingSystemParsing($userAgent, array $jsUserAgent, $family, $major, $minor, $patch, $patchMinor)
    {
        $result = $this->parser->parse($userAgent, $jsUserAgent);

        $this->assertSame($family, $result->os->family);
        $this->assertSame($major, $result->os->major);
        $this->assertSame($minor, $result->os->minor);
        $this->assertSame($patch, $result->os->patch);
        $this->assertSame($patchMinor, $result->os->patchMinor);
    }

    /** @dataProvider getUserAgentTestData */
    public function testUserAgentParsing($userAgent, array $jsUserAgent, $family, $major, $minor, $patch)
    {
        $result = $this->parser->parse($userAgent, $jsUserAgent);

        $this->assertSame($family, $result->ua->family);
        $this->assertSame($major, $result->ua->major);
        $this->assertSame($minor, $result->ua->minor);
        $this->assertSame($patch, $result->ua->patch);
    }

    /** @group performance */
    public function testPerformance()
    {
        $userAgents = array(
            'Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en; rv:1.8.1.6) Gecko/20070809 Camino/1.5.1',
            'Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko',
            'Mozilla/5.0 (iPod; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5',
            'Mozilla/5.0 (X11; CrOS i686 13.587.80) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.99 Safari/535.1'
        );

        $start = microtime(true) * 1000;
        $rounds = 0;
        for ($a = 0; $a < 1000; $a++) {
            foreach ($userAgents as $userAgent) {
                $parser = Parser::create();
                $this->assertNotSame('Other', $parser->parse($userAgent)->ua->family);
                $rounds++;
            }
        }
        $end = microtime(true) * 1000;

        $duration = $end - $start;
        $expected = 1.5;
        $this->assertLessThan(
            $expected,
            $duration/ $rounds,
            sprintf('Should not take longer on average than %dms (%d rounds, %Fms)', $expected, $rounds, $duration)
        );
    }

    public function testExceptionOnFileNotFound()
    {
        $this->setExpectedException(
            'UAParser\Exception\FileNotFoundException',
            'ua-parser cannot find the custom regexes file you supplied ("invalidFile"). Please make sure you have the correct path.'
        );

        new Parser('invalidFile');
    }

    /** @deprecated */
    public function testExceptionOnFileNotFoundInvalidDefault()
    {
        $this->setExpectedException(
            'UAParser\Exception\FileNotFoundException',
            'Please download the "invalidFile" file before using ua-parser by running "php bin/uaparser.php uaparser:update"'
        );

        Parser::$defaultFile = 'invalidFile';
        new Parser();
    }

    public function testToString()
    {
        $userAgentString = 'HbbTV/1.1.1 (;;;;;) firetv-firefox-plugin 1.1.20';
        $result = $this->parser->parse($userAgentString);

        $this->assertSame('HbbTV 1.1.1/FireHbbTV 1.1.20', $result->toString());
        $this->assertSame('HbbTV 1.1.1/FireHbbTV 1.1.20', (string) $result);

        $this->assertSame('HbbTV 1.1.1', $result->ua->toString());
        $this->assertSame('HbbTV 1.1.1', (string) $result->ua);
        $this->assertSame('1.1.1', $result->ua->toVersion());

        $this->assertSame('FireHbbTV 1.1.20', $result->os->toString());
        $this->assertSame('FireHbbTV 1.1.20', (string) $result->os);
        $this->assertSame('1.1.20', $result->os->toVersion());

        $this->assertSame($userAgentString, $result->originalUserAgent);
    }

    public function testToString_2()
    {
        $userAgentString = 'PingdomBot 1.4/Other Pingdom.com_bot_version_1.4_(http://www.pingdom.com/)';

        $result = $this->parser->parse($userAgentString);

        $this->assertSame('PingdomBot 1.4/Other', $result->toString());
        $this->assertSame('PingdomBot 1.4/Other', (string) $result);

        $this->assertSame('PingdomBot 1.4', $result->ua->toString());
        $this->assertSame('PingdomBot 1.4', (string) $result->ua);
        $this->assertSame('1.4', $result->ua->toVersion());

        $this->assertSame('Other', $result->os->toString());
        $this->assertSame('Other', (string) $result->os);
        $this->assertSame('', $result->os->toVersion());

        $this->assertSame($userAgentString, $result->originalUserAgent);
    }

    /** @deprecated */
    public function testCreateWithDeprecatedConstructorCustom()
    {
        $parserClassName = $this->getParserClassName();

        $this->setExpectedException(
            'PHPUnit_Framework_Error_Deprecated',
            'Using the constructor is deprecated. Use Parser::create(string $file = null) instead'
        );
        new $parserClassName(__DIR__ . '/../../../resources/regexes.php');
    }

    private static function createTestData(Finder $resources)
    {
        $resourcesDirectory = __DIR__ . '/../../../../test_resources';
        $testData = array();

        /** @var $resource SplFileInfo */
        foreach ($resources->in($resourcesDirectory) as $resource) {
            $data = Yaml::parse($resource->getContents());
            foreach ($data['test_cases'] as $testCase) {
                $testData[] = static::createArguments($testCase, $resource);
            }
        }

        return $testData;
    }

    private static function createArguments(array $testCase, SplFileInfo $resource)
    {
        return array(
            $testCase['user_agent_string'],
            isset($testCase['js_ua']) ? json_decode(str_replace("'", '"', $testCase['js_ua']), true) : array(),
            $testCase['family'],
            isset($testCase['major']) ? $testCase['major'] : null,
            isset($testCase['minor']) ? $testCase['minor'] : null,
            isset($testCase['patch']) ? $testCase['patch'] : null,
            isset($testCase['patch_minor']) ? $testCase['patch_minor'] : null,
            $resource->getFilename()
        );
    }

    protected function getParserClassName()
    {
        return 'UAParser\Parser';
    }
}
