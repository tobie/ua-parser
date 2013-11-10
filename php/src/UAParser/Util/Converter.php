<?php
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

    public function __construct($destination, Filesystem $fs = null)
    {
        $this->destination = $destination;
        $this->fs = $fs ? $fs : new Filesystem();
    }

    public function convertFile($yamlFile, $backupBeforeOverride = true)
    {
        if (!$this->fs->exists($yamlFile)) {
            throw FileNotFoundException::create($yamlFile);
        }

        $this->doConvert(Yaml::parse(file_get_contents($yamlFile)), $backupBeforeOverride);
    }

    public function convertString($yamlString, $backupBeforeOverride = true)
    {
        $this->doConvert(Yaml::parse($yamlString), $backupBeforeOverride);
    }

    protected function doConvert(array $regexes, $backupBeforeOverride = true)
    {
        $data = json_encode($regexes);

        $regexesFile = $this->destination . '/regexes.json';
        if ($backupBeforeOverride && $this->fs->exists($regexesFile)) {

            $currentHash = hash('sha512', file_get_contents($regexesFile));
            $futureHash = hash('sha512', $data);

            if ($futureHash === $currentHash) {
                return;
            }

            $backupFile = $this->destination . '/regexes-' . $currentHash . '.json';
            $this->fs->copy($regexesFile, $backupFile);
        }

        $this->fs->dumpFile($regexesFile, $data);
    }
}
