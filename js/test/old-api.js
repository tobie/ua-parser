// Documenting current behaviour for backwards compat reqs.

var assert = require('assert'),
    parse = require('../index').parse;

var USER_AGENT_STRING = 'Mozilla/5.0 (Windows NT 6.1; rv:2.0b6pre) Gecko/20100903 Firefox/4.0b6pre Firefox/4.0b6pre';

suite('parse function', function() {
  test('output with valid UA string', function() {
    var output = parse(USER_AGENT_STRING);
    assert.strictEqual(output.family, 'Firefox Beta');
    assert.strictEqual(output.major, 4);
    assert.strictEqual(output.minor, 0);
    assert.ok(isNaN(output.patch));
  });

  test('output with invalid UA string', function() {
    var output = parse('');
    assert.strictEqual(output.family, 'Other');
    assert.strictEqual(output.major, undefined);
    assert.strictEqual(output.minor, undefined);
    assert.strictEqual(output.patch, undefined);
  });
});

