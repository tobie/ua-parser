<?php
namespace UAParser\Tests\Util;

use PHPUnit_Framework_TestCase as AbstractTestCase;
use PHPUnit_Framework_MockObject_MockObject as MockObject;
use Symfony\Component\Filesystem\Filesystem;
use UAParser\Util\Converter;

class ConverterTest extends AbstractTestCase
{
    /** @var Filesystem|MockObject */
    private $fs;

    /** @var Converter */
    private $converter;

    /** @var string */
    private $yamlFile;

    public function setUp()
    {
        $this->fs = $this
            ->getMockBuilder('Symfony\Component\Filesystem\Filesystem')
            ->disableOriginalConstructor()
            ->disableOriginalClone()
            ->getMock();
        $this->converter = new Converter('path/to/destination', $this->fs);
        $yaml = <<<EOS
foo:
    bar
EOS;
        $this->yamlFile = sys_get_temp_dir() . '/uaparser-' . time();
        file_put_contents($this->yamlFile, $yaml);
    }

    public function tearDown()
    {
        @unlink($this->yamlFile);
    }

    public function testExceptionIsThrownIfFileDoesNotExist()
    {
        $this->fs
            ->expects($this->once())
            ->method('exists')
            ->with('path/to/file')
            ->will($this->returnValue(false));

        $this->setExpectedException(
            'UAParser\Exception\FileNotFoundException',
            'File "path/to/file" does not exist'
        );
        $this->converter->convert('path/to/file');
    }

    public function testFileIsBackedUpIfExists()
    {
        $this->fs
            ->expects($this->at(0))
            ->method('exists')
            ->with($this->yamlFile)
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->at(1))
            ->method('exists')
            ->with('path/to/destination/regexes.json')
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->once())
            ->method('copy')
            ->with(
                'path/to/destination/regexes.json',
                $this->matchesRegularExpression('@path/to/destination/regexes.\d+.json@')
            )
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->once())
            ->method('dumpFile')
            ->with('path/to/destination/regexes.json', '{"foo":"bar"}');

        $this->converter->convert($this->yamlFile);
    }

    public function testFileIsNotBackedUp()
    {
        $this->fs
            ->expects($this->once())
            ->method('exists')
            ->with($this->yamlFile)
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->never())
            ->method('copy');

        $this->fs
            ->expects($this->once())
            ->method('dumpFile')
            ->with('path/to/destination/regexes.json', '{"foo":"bar"}');

        $this->converter->convert($this->yamlFile, false);
    }
}
