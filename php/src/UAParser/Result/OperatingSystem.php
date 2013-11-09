<?php
namespace UAParser\Result;

class OperatingSystem extends AbstractArtifact
{
    /** @var string */
    public $major;

    /** @var string */
    public $minor;

    /** @var string */
    public $patch;

    /** @var string */
    public $patchMinor;

    public function toVersion()
    {
        return $this->formatVersion($this->major, $this->minor, $this->patch, $this->patchMinor);
    }
}
