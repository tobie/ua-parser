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
    protected function configure()
    {
        $this
            ->setName('ua-parser:update')
            ->setDescription('Fetches an updated YAML file for ua-parser and overwrites the current JSON file.')
            ->addArgument(
                'file',
                InputArgument::REQUIRED,
                'regexes.yaml output file'
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
