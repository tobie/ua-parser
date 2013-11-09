<?php
namespace UAParser\Tests;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use Symfony\Component\Finder\Finder;
use Symfony\Component\Finder\SplFileInfo;
use Symfony\Component\Yaml\Yaml;
use UAParser\Parser;

class ParserTest extends AbstractTestCase
{
    /** @var Parser */
    private $parser;

    /** @var Parser */
    private static $staticParser;

    public static function setUpBeforeClass()
    {
        static::$staticParser = new Parser();
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
        $this->assertSame($patchMinor, $result->os->patch_minor);
    }
    /** @dataProvider getUserAgentTestData */
    public function testUserAgentParsing($userAgent, array $jsUserAgent, $family, $major, $minor, $patch, $patchMinor)
    {
        $result = $this->parser->parse($userAgent, $jsUserAgent);

        $this->assertSame($family, $result->ua->family);
        $this->assertSame($major, $result->ua->major);
        $this->assertSame($minor, $result->ua->minor);
        $this->assertSame($patch, $result->ua->patch);

        if ($patchMinor) {

            $this->assertSame($patchMinor, $result->ua->patch_minor);

        }
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
}
