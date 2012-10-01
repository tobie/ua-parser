/*jshint evil: true */
var path = require('path'),
	fs = require('fs'),
	yaml = require('yamlparser'),
	UserAgent = require('./UserAgent'),
	file, regexes, input, ua;

file = path.join(__dirname, '..', 'regexes.yaml');
regexes = fs.readFileSync(file, 'utf8');
regexes = yaml['eval'](regexes);

UserAgent.setupRegexes(regexes);

if (require.main === module) {
	input = process.argv[2];
	if (!input) {
		process.exit(1);
	}
	ua = UserAgent.parse(input);
	if (!ua) {
		process.exit(1);
	}

	console.log(ua.toString());
	console.dir(ua);
}

module.exports = {
	parse: UserAgent.parse,
	UserAgent: UserAgent
};
