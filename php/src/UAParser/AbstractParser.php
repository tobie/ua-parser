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

use UAParser\Exception\FileNotFoundException;

abstract class AbstractParser
{
    /** @var string */
    public static $defaultFile;

    /** @var array */
    protected $regexes = array();

    public function __construct(array $regexes)
    {
        $this->regexes = $regexes;
    }

    /**
     * Create parser instance
     *
     * Either pass a custom regexes.php file or leave the argument empty and use the default file.
     *
     * @param string $file
     * @throws FileNotFoundException
     * @return static
     */
    public static function create($file = null)
    {
        return $file ? static::createCustom($file) : static::createDefault();
    }

    /**
     * @return static
     * @throws FileNotFoundException
     */
    protected static function createDefault()
    {
        return static::createInstance(
            static::getDefaultFile(),
            array('UAParser\Exception\FileNotFoundException', 'defaultFileNotFound')
        );
    }

    /**
     * @return static
     * @throws FileNotFoundException
     */
    protected static function createCustom($file)
    {
        return static::createInstance(
            $file,
            array('UAParser\Exception\FileNotFoundException', 'customRegexFileNotFound')
        );
    }

    private static function createInstance($file, $exceptionFactory)
    {
        if (!file_exists($file)) {
            throw call_user_func($exceptionFactory, $file);
        }

        return new static(include $file);
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
    protected function replaceString(array $regex, $key, $string)
    {
        if (!isset($regex[$key])) {
            return $string;
        }

        return str_replace('$1', $string, $regex[$key]);
    }

    /**
     * @param array $regex
     * @param string $key
     * @param string $default
     * @param array $matches
     * @return string
     */
    protected function multiReplace(array $regex, $key, $default, array $matches)
    {
        if (!isset($regex[$key])) {
            return $default;
        }
        
        $replacement = preg_replace_callback(
            "|\\$(?<key>\d)|",
            function ($m) use ($matches){
                return isset($matches[$m['key']]) ? $matches[$m['key']] : "";
            },
            $regex[$key]
        );
        
        return empty($replacement) ? null : $replacement;
    }

    /**
     * @return string
     */
    protected static function getDefaultFile()
    {
        return static::$defaultFile
            ? static::$defaultFile
            : realpath(__DIR__ . '/../../resources') . DIRECTORY_SEPARATOR . 'regexes.php';
    }
}
