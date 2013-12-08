<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2013 Dave Olsen, http://dmolsen.com
 * Copyright (c) 2013-2014 Lars Strojny, http://usrportage.de
 *
 * Released under the MIT license
 */
namespace UAParser;

abstract class AbstractParser
{
    /** @var string */
    public static $defaultFile;

    /** @var array */
    protected $regexes;

    public function __construct(array $regexes)
    {
        $this->regexes = $regexes;
    }

    /**
     * @param array $regexes
     * @param string $userAgent
     * @return array
     */
    protected function tryMatch(array $regexes, $userAgent)
    {
        foreach ($regexes as $regex) {
            if (preg_match('@' . $regex['regex'] . '@', $userAgent, $matches)) {

                $defaults = array(
                    1 => 'Other',
                    2 => null,
                    3 => null,
                    4 => null,
                    5 => null,
                );

                return array($regex, $matches + $defaults);
            }
        }

        return array(null, null);
    }

    /**
     * @param array $regex
     * @param string $key
     * @param string $string
     * @return string
     */
    protected function replaceString($regex, $key, $string)
    {
        if (!isset($regex[$key])) {
            return $string;
        }

        return str_replace('$1', $string, $regex[$key]);
    }

    protected static function getDefaultFile()
    {
        return static::$defaultFile ? static::$defaultFile : __DIR__ . '/../../resources/regexes.php';
    }
}
