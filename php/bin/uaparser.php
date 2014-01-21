<?php
namespace UAParser\Command;

use Symfony\Component\Console\Application;

$packageAutoloader = __DIR__ . '/../../vendor/autoload.php';
$standaloneAutoloader = __DIR__ . '/../../../../autoload.php';
if (file_exists($packageAutoloader)) {
    require_once $packageAutoloader;
} else {
    require_once $standaloneAutoloader;
}

$resourceDirectory = realpath(__DIR__ . '/../resources');
$defaultYamlFile = realpath(__DIR__ . '/../../regexes.yaml');

$application = new Application('ua-parser');
$application->add(new ConvertCommand($resourceDirectory, $defaultYamlFile));
$application->add(new UpdateCommand($resourceDirectory));
$application->add(new ParserCommand());
$application->add(new LogfileCommand());
$application->add(new FetchCommand($defaultYamlFile));

$application->run();
