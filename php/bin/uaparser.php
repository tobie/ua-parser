<?php
namespace UAParser\Command;

use Symfony\Component\Console\Application;

require_once __DIR__ . '/../../vendor/autoload.php';

$resourceDirectory = realpath(__DIR__ . '/../resources');
$defaultYamlFile = realpath(__DIR__ . '/../../regexes.yaml');

$application = new Application('ua-parser');
$application->add(new ConvertCommand($resourceDirectory, $defaultYamlFile));
$application->add(new UpdateCommand($resourceDirectory));
$application->add(new ParserCommand());
$application->add(new LogfileCommand());
$application->add(new FetchCommand($defaultYamlFile));

$application->run();
