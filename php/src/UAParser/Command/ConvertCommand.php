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
use Symfony\Component\Console\Input\InputOption;
use Symfony\Component\Console\Output\OutputInterface;
use UAParser\Util\Converter;

class ConvertCommand extends Command
{
    /** @var string */
    private $resourceDirectory;

    /** @var string */
    private $defaultYamlFile;

    public function __construct($resourceDirectory, $defaultYamlFile)
    {
        $this->resourceDirectory = $resourceDirectory;
        $this->defaultYamlFile = $defaultYamlFile;
        parent::__construct();
    }

    protected function configure()
    {
        $this
            ->setName('ua-parser:convert')
            ->setDescription('Converts an existing regexes.yaml file to a regexes.php file.')
            ->addArgument(
                'file',
                InputArgument::OPTIONAL,
                'Path to the regexes.yaml file',
                $this->defaultYamlFile
            )
            ->addOption(
                'no-backup',
                null,
                InputOption::VALUE_NONE,
                'Do not backup the previously existing file'
            )
        ;
    }

    protected function execute(InputInterface $input, OutputInterface $output)
    {
        $this->getConverter()->convertFile($input->getArgument('file'), $input->getOption('no-backup'));
    }

    private function getConverter()
    {
        return new Converter($this->resourceDirectory);
    }
}
