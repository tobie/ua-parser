<?php
namespace UAParser\Result;

abstract class AbstractResult
{
    abstract public function toString();

    public function __toString()
    {
        return $this->toString();
    }
}
