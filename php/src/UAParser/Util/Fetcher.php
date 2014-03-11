<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util;

use UAParser\Exception\FetcherException;

class Fetcher
{
    private $resourceUri = 'https://raw.github.com/tobie/ua-parser/master/regexes.yaml';

    /** @var resource */
    private $streamContext;

    public function __construct($streamContext = null)
    {
        if (is_resource($streamContext) && get_resource_type($streamContext) === 'stream-context') {
            $this->streamContext = $streamContext;
        } else {
            $this->streamContext = stream_context_create(
                array(
                    'ssl' => array(
                        'verify_peer'         => true,
                        'verify_depth'        => 5,
                        'cafile'              => __DIR__ . '/../../../resources/ca-bundle.crt',
                        'CN_match'            => 'www.github.com',
                        'disable_compression' => true,
                    )
                )
            );
        }
    }

    public function fetch()
    {
        $level = error_reporting(0);
        $result = file_get_contents($this->resourceUri, null, $this->streamContext);
        error_reporting($level);

        if ($result === false) {
            $error = error_get_last();
            throw FetcherException::httpError($this->resourceUri, $error['message']);
        }

        return $result;
    }
}
