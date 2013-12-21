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

use UAParser\Result\OperatingSystem;

class OperatingSystemParser extends AbstractParser
{
    /**
     * Attempts to see if the user agent matches an operating system regex from regexes.php
     *
     * @param string $userAgent a user agent string to test
     * @return OperatingSystem
     */
    public function parseOperatingSystem($userAgent)
    {
        $os = new OperatingSystem();

        list($regex, $matches) = $this->tryMatch($this->regexes['os_parsers'], $userAgent);

        if ($matches) {
            $os->family = $this->replaceString($regex, 'os_replacement', $matches[1]);
            $os->major = $this->replaceString($regex, 'os_v1_replacement', $matches[2]);
            $os->minor = $this->replaceString($regex, 'os_v2_replacement', $matches[3]);
            $os->patch = $this->replaceString($regex, 'os_v3_replacement', $matches[4]);
            $os->patchMinor = $this->replaceString($regex, 'os_v4_replacement', $matches[5]);
        }

        return $os;
    }
}
