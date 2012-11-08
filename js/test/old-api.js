// Documenting current behaviour for backwards compat reqs.

var assert = require('assert'),
    parse = require('../index').parse;

var USER_AGENT_STRING = 'Mozilla/5.0 (Windows NT 6.1; rv:2.0b6pre) Gecko/20100903 Firefox/4.0b6pre Firefox/4.0b6pre';

suite('parse function', function() {  
  test("output with valid UA string", function() {
    var output = parse(USER_AGENT_STRING);
    assert.strictEqual('Firefox Beta', output.family);
    assert.strictEqual(4, output.major, 'major version number of the UA');
    assert.strictEqual(0, output.minor, 'minor version number of the UA');
    assert.ok(isNaN(output.patch),  'patch version number of the UA');
  });
  
  test("output with invalid UA string", function() {
    var output = parse('');
    assert.strictEqual('Other', output.family);
    assert.strictEqual(undefined, output.major);
    assert.strictEqual(undefined, output.minor);
    assert.strictEqual(undefined, output.patch);
  });
});

