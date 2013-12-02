<?php
/**
 * ua-parser
 *
 * Copyright (c) 2011-2012 Dave Olsen, http://dmolsen.com
 *
 * Released under the MIT license
 */
namespace UAParser\Command;

use Symfony\Component\Console\Command\Command;
use Symfony\Component\Console\Input\InputArgument;
use Symfony\Component\Console\Input\InputInterface;
use Symfony\Component\Console\Output\OutputInterface;
use Symfony\Component\Filesystem\Filesystem;
use UAParser\Util\Fetcher;

class FetchCommand extends Command
{
    /** @var string */
    private $defaultYamlFile;

    public function __construct($defaultYamlFile)
    {
        $this->defaultYamlFile = $defaultYamlFile;
        parent::__construct();
    }

    protected function configure()
    {
        $this
            ->setName('ua-parser:fetch')
            ->setDescription('Fetches an updated YAML file for ua-parser.')
            ->addArgument(
                'file',
                InputArgument::OPTIONAL,
                'regexes.yaml output file',
                $this->defaultYamlFile
            )
        ;
    }

    protected function execute(InputInterface $input, OutputInterface $output)
    {
        $fs = new Filesystem();
        $fetcher = new Fetcher();
        $fs->dumpFile($input->getArgument('file'), $fetcher->fetch());
    }
}
