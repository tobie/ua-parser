<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Util;

class Fetcher
{
    public function fetch()
    {
        $context = stream_context_create(
            array(
                'ssl' => array(
                    'verify_peer'         => true,
                    'verify_depth'        => 5,
                    'cafile'              => __DIR__ . '/../../../resources/ca-bundle.crt',
                    'CN_match'            => 'raw.github.com',
                    'disable_compression' => true,
                )
            )
        );

        return file_get_contents('https://raw.github.com/tobie/ua-parser/master/regexes.yaml', null, $context);
    }
}
