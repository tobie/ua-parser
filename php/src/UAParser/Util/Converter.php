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

    public function convert($yamlFile, $backupIfExists = true)
    {
        if (!$this->fs->exists($yamlFile)) {
            throw FileNotFoundException::create($yamlFile);
        }

        $regexesFile = $this->destination . '/regexes.json';
        if ($backupIfExists && $this->fs->exists($regexesFile)) {
            $backupFile = $this->destination . '/regexes.' . time() . '.json';
            $this->fs->copy($regexesFile, $backupFile);
        }

        $data = Yaml::parse(file_get_contents($yamlFile));
        $this->fs->dumpFile($regexesFile, json_encode($data));
    }
}
