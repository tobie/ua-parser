<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util\Logfile;

class ApacheCommonLogFormatReader extends AbstractReader
{
    protected function getRegex()
    {
        return '@^
            (?:\S+)                                                 # IP
            \s+
            (?:\S+)
            \s+
            (?:\S+)
            \s+
            \[(?:[^:]+):(?:\d+:\d+:\d+) \s+ (?:[^\]]+)\]            # Date/time
            \s+
            \"(?:\S+)\s(?:.*?)                                      # Verb
            \s+
            (?:\S+)\"                                               # Path
            \s+
            (?:\S+)                                                 # Response
            \s+
            (?:\S+)                                                 # Length
            \s+
            (?:\".*?\")                                             # Referrer
            \s+
            \"(?P<userAgentString>.*?)\"                            # User Agent
        $@x';
    }
}
