<?php
namespace UAParser\Util;

class Updater
{
    /** @var Converter */
    private $converter;

    public function __construct(Converter $converter)
    {
        $this->converter = $converter;
    }

    public function update($backupBeforeOverride = true)
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

        $this->converter->convertString(
            file_get_contents('https://raw.github.com/tobie/ua-parser/master/regexes.yaml', null, $context),
            $backupBeforeOverride
        );
    }
}
