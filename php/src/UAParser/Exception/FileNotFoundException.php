<?php
namespace UAParser\Exception;

use Exception;

class FileNotFoundException extends Exception
{
    public static function create($file)
    {
        return new static(sprintf('File "%s" does not exist', $file));
    }
}
