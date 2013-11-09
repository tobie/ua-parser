<?php
namespace UAParser\Result;

abstract class AbstractDevice extends AbstractResult
{
    /** @var string */
    public $family = 'Other';

    public function toString()
    {
        return $this->family;
    }
}
