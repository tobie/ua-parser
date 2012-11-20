<?php
/**
 * Simple utility script to update the UAParser definitions from a command line
 * usage: uaparse.php -get
 */
require __DIR__.DIRECTORY_SEPARATOR.'UAParser.php';

if (php_sapi_name() == 'cli') {
	if ($argc > 1 and $argv[1] == '-get') {
		UA::$silent   = ((isset($argv[2]) && ($argv[2] == '-silent')) || (isset($argv[3]) && ($argv[3] == '-silent'))) ? true : UA::$silent;
		UA::$nobackup = ((isset($argv[2]) && ($argv[2] == '-nobackup')) || (isset($argv[3]) && ($argv[3] == '-nobackup'))) ? true : UA::$nobackup;
		if (!UA::$silent) {
			print("getting the YAML file...\n");
		}
		UA::get();
	} elseif ($argc == 2) {
		var_dump(UA::parse($argv[1]));
	}
} else {
	print("You must use the -get flag to use UAParser.php from the command line.\n");
}
