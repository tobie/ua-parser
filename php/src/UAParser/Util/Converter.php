<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util;

use Symfony\Component\Filesystem\Filesystem;
use Symfony\Component\Yaml\Yaml;
use UAParser\Exception\FileNotFoundException;

class Converter
{
    /** @var string */
    private $destination;

    /** @var Filesystem */
    private $fs;

    /**
     * @param string $destination
     * @param Filesystem $fs
     */
    public function __construct($destination, Filesystem $fs = null)
    {
        $this->destination = $destination;
        $this->fs = $fs ? $fs : new Filesystem();
    }

    /**
     * @param string $yamlFile
     * @param bool $backupBeforeOverride
     * @throws FileNotFoundException
     */
    public function convertFile($yamlFile, $backupBeforeOverride = true)
    {
        if (!$this->fs->exists($yamlFile)) {
            throw FileNotFoundException::fileNotFound($yamlFile);
        }

        $this->doConvert(Yaml::parse(file_get_contents($yamlFile)), $backupBeforeOverride);
    }

    /**
     * @param string $yamlString
     * @param bool $backupBeforeOverride
     */
    public function convertString($yamlString, $backupBeforeOverride = true)
    {
        $this->doConvert(Yaml::parse($yamlString), $backupBeforeOverride);
    }

    /**
     * @param array $regexes
     * @param bool $backupBeforeOverride
     */
    protected function doConvert(array $regexes, $backupBeforeOverride = true)
    {
        $data = "<?php\nreturn " . var_export($regexes, true) . ';';

        $regexesFile = $this->destination . '/regexes.php';
        if ($backupBeforeOverride && $this->fs->exists($regexesFile)) {

            $currentHash = hash('sha512', file_get_contents($regexesFile));
            $futureHash = hash('sha512', $data);

            if ($futureHash === $currentHash) {
                return;
            }

            $backupFile = $this->destination . '/regexes-' . $currentHash . '.php';
            $this->fs->copy($regexesFile, $backupFile);
        }

        $this->fs->dumpFile($regexesFile, $data);
    }
}
