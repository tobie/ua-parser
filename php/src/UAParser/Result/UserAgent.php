<?php
namespace UAParser\Result;

class UserAgent extends AbstractArtifact
{
    /** @var string */
    public $major;

    /** @var string */
    public $minor;

    /** @var string */
    public $patch;

    public function toVersion()
    {
        return $this->formatVersion($this->major, $this->minor, $this->patch);
    }
}
