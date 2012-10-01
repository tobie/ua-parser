/*jshint evil: true */
var path = require('path'),
	fs = require('fs'),
	yaml = require('yamlparser'),
	file, regexes;

file = path.join(__dirname, '..', 'regexes.yaml');
regexes = fs.readFileSync(file, 'utf8');
regexes = yaml['eval'](regexes);

fs.writeFile(
	'UserAgent.init.js',
	'UserAgent.setupRegexes(' +
		(process.argv[2] === '--min' ?
			JSON.stringify(regexes) :
			JSON.stringify(regexes, null, '\t')
		) +
		');\n',
	function (err) {
		if (err) {
			console.error(err);
			process.exit(1);
		}
	}
);
