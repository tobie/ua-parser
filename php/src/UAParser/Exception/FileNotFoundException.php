<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Exception;

use Exception;

class FileNotFoundException extends Exception
{
    public static function fileNotFound($file)
    {
        return new static(sprintf('File "%s" does not exist', $file));
    }

    public static function customRegexFileNotFound($file)
    {
        return new static(
            sprintf(
                'ua-parser cannot find the custom regexes file you supplied ("%s"). Please make sure you have the correct path.',
                $file
            )
        );
    }

    public static function defaultFileNotFound($file)
    {
        return new static(
            sprintf(
                'Please download the "%s" file before using ua-parser by running "php bin/uaparser.php ua-parser:update"',
                $file
            )
        );
    }
}
