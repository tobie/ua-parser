var path = require('path'),
  fs = require('fs'),
  yamlparser = require('yamlparser'),

  results = require('./lib/results'),
  ua = require('./lib/ua'),
  os = require('./lib/os'),
  device = require('./lib/device'),

  regexes = yamlparser.eval(
    fs.readFileSync(
      path.join(__dirname, '..', 'regexes.yaml'),
    'utf8')
  ),

  parseUA =
    exports.parseUA = ua.makeParser(regexes.user_agent_parsers),
  parseOS =
    exports.parseOS = os.makeParser(regexes.os_parsers),
  parseDevice =
    exports.parseDevice = device.makeParser(regexes.device_parsers);

exports.parse = function(str) {
  return new results.BackwardsCompatResults(str,
    parseUA(str),
      parseOS(str),
        parseDevice(str));
};

if (require.main === module) {
  process.stdout.write(
    parseUA(process.argv[2] || process.exit(1)).toString()
  );
}
