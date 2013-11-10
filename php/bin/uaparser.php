<?php
namespace UAParser\Command;

use Symfony\Component\Console\Application;

require_once __DIR__ . '/../../vendor/autoload.php';

$application = new Application('ua-parser');
$application->add(new ConvertCommand(realpath(__DIR__ . '/../resources'), realpath(__DIR__ . '/../../regexes.yaml')));
$application->add(new UpdateCommand(realpath(__DIR__ . '/../resources')));
$application->add(new ParserCommand());
$application->add(new LogfileCommand());
$application->add(new FetchCommand(realpath(__DIR__ . '/../../regexes.yaml')));

$application->run();
