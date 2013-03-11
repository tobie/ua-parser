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

function msg(name, actual, expected) {
  return "Expected " + name + " to be " + JSON.stringify(expected) + " got " + JSON.stringify(actual) + " instead.";
}

['firefox_user_agent_strings.yaml', 'test_user_agent_parser.yaml', 'pgts_browser_list.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      if (f.js_ua) return;
      test(f.user_agent_string, function() {
        var ua = uaParser.parse(f.user_agent_string).userAgent;
        fixFixture(f, ['major', 'minor', 'patch']);
        assert.strictEqual(ua.family, f.family, msg('ua.family', ua.family, f.family));
        assert.strictEqual(ua.major, f.major, msg('ua.major', ua.major, f.major));
        assert.strictEqual(ua.minor, f.minor, msg('ua.minor', ua.minor, f.minor));
        assert.strictEqual(ua.patch, f.patch, msg('ua.patch', ua.patch, f.patch));
      });
    });
  });
});

['test_user_agent_parser_os.yaml', 'additional_os_tests.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      test(f.user_agent_string, function() {
        var os = uaParser.parse(f.user_agent_string).os;
        fixFixture(f, ['major', 'minor', 'patch', 'patch_minor']);
        assert.strictEqual(os.family, f.family, msg('os.family', os.family, f.family));
        assert.strictEqual(os.major, f.major, msg('os.major', os.major, f.major));
        assert.strictEqual(os.minor, f.minor, msg('os.minor', os.minor, f.minor));
        assert.strictEqual(os.patch, f.patch, msg('os.patch', os.patch, f.patch));
        assert.strictEqual(os.patchMinor, f.patch_minor, msg('os.patchMinor', os.patchMinor, f.patch_minor));
      });
    });
  });
});

['test_device.yaml'].forEach(function(fileName) {
  var fixtures = readYAML(fileName).test_cases;
  suite(fileName, function() {
    fixtures.forEach(function(f) {
      test(f.user_agent_string, function() {
        var device = uaParser.parse(f.user_agent_string).device;
        assert.strictEqual(device.family, f.family, msg('device.family', device.family, f.family));
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
