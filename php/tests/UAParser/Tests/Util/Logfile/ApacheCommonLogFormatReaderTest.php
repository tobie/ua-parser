<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Tests\Util\Logfile;

use UAParser\Util\Logfile\ApacheCommonLogFormatReader;

class ApacheCommonLogFormatReaderTest extends AbstractReaderTest
{
    public function setUp()
    {
        $this->reader = new ApacheCommonLogFormatReader();

        $this->line = <<<EOS
127.0.0.1 - - [20/Nov/2013:04:14:44 +0100] "GET / HTTP/1.1" 304 - "-" "Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.0.19; aggregator:Spinn3r (Spinn3r 3.1); http://spinn3r.com/robot) Gecko/2010040121 Firefox/3.0.19"
EOS;
        $this->userAgentString = 'Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.0.19; aggregator:Spinn3r (Spinn3r 3.1); http://spinn3r.com/robot) Gecko/2010040121 Firefox/3.0.19';

        // Set your example log file
        //$this->exampleLogFile = '/var/log/access.log';
    }
}
