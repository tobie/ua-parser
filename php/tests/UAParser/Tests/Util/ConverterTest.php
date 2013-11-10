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

    /** @var string */
    private $jsonFile;

    public function setUp()
    {
        $this->fs = $this
            ->getMockBuilder('Symfony\Component\Filesystem\Filesystem')
            ->disableOriginalConstructor()
            ->disableOriginalClone()
            ->getMock();
        $this->converter = new Converter(sys_get_temp_dir(), $this->fs);
        $yaml = <<<EOS
foo:
    bar
EOS;
        $this->yamlFile = sys_get_temp_dir() . '/uaparser-' . time() . '.yaml';
        $this->jsonFile = sys_get_temp_dir() . '/regexes.json';
        touch($this->jsonFile);
        file_put_contents($this->yamlFile, $yaml);
    }

    public function tearDown()
    {
        @unlink($this->yamlFile);
        @unlink($this->jsonFile);
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
        $this->converter->convertFile('path/to/file');
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
            ->with($this->jsonFile)
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->once())
            ->method('copy')
            ->with(
                $this->jsonFile,
                $this->matchesRegularExpression('@/regexes-.{128}\.json@')
            )
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->once())
            ->method('dumpFile')
            ->with($this->jsonFile, '{"foo":"bar"}');

        $this->converter->convertFile($this->yamlFile);
    }

    public function testFileIsNotBackedUpIfHashesDoNotMatch()
    {
        file_put_contents($this->jsonFile, '{"foo":"bar"}');

        $this->fs
            ->expects($this->at(0))
            ->method('exists')
            ->with($this->yamlFile)
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->at(1))
            ->method('exists')
            ->with($this->jsonFile)
            ->will($this->returnValue(true));

        $this->fs
            ->expects($this->never())
            ->method('copy');

        $this->fs
            ->expects($this->never())
            ->method('dumpFile');

        $this->converter->convertFile($this->yamlFile);
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
            ->with($this->jsonFile, '{"foo":"bar"}');

        $this->converter->convertFile($this->yamlFile, false);
    }
}
