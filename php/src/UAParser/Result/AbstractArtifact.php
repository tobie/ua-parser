<?php
namespace UAParser\Result;

abstract class AbstractArtifact extends AbstractDevice
{
    abstract public function toVersion();

    public function toString()
    {
        return $this->family . ' ' . $this->toVersion();
    }

    protected function formatVersion()
    {
        return join('.', array_filter(func_get_args()));
    }
}
