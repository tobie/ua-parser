var assert = require('assert'),
    path = require('path'), 
    fs = require('fs'),
    yaml = require('yamlparser'),
    uaParser = require('../index');

function readYAML(fileName) {
  var file = path.join(__dirname, '..', '..', 'test_resources', fileName);
  var fixtures = fs.readFileSync(file, 'utf8');
  fixtures = yaml.eval(fixtures);
  return fixtures;
}

['firefox_user_agent_strings.yaml', 'test_user_agent_parser.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      if (f.js_ua) return;
      test(f.user_agent_string, function() {
        var r = uaParser.parse(f.user_agent_string);
        fixFixture(f, ['major', 'minor', 'patch']);
        assert.strictEqual(r.userAgent.family, f.family);
        assert.strictEqual(r.userAgent.major, f.major);
        assert.strictEqual(r.userAgent.minor, f.minor);
        assert.strictEqual(r.userAgent.patch, f.patch);
      });
    });
  });
});

['test_user_agent_parser_os.yaml', 'additional_os_tests.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      test(f.user_agent_string, function() {
        var r = uaParser.parse(f.user_agent_string);
        fixFixture(f, ['major', 'minor', 'patch', 'patch_minor']);
        assert.strictEqual(r.os.family, f.family);
        assert.strictEqual(r.os.major, f.major);
        assert.strictEqual(r.os.minor, f.minor);
        assert.strictEqual(r.os.patch, f.patch);
        assert.strictEqual(r.os.patchMinor, f.patch_minor);
      });
    });
  });
});

['test_device.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      test(f.user_agent_string, function() {
        var r = uaParser.parse(f.user_agent_string);
        assert.strictEqual(r.device.family, f.family);
      });
    });
  });
});

function fixFixture(f, props) {
  // A bug in the YAML parser makes empty fixture props
  // return a vanila object.
  props.forEach(function(p) {
    if (typeof f[p] === 'object') {
      f[p] = null;
    }
  })
  return f;
}
