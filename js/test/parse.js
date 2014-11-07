var assert = require('assert'),
    parse = require('../index').parse;

var USER_AGENT_STRING = 'Mozilla/5.0 (Windows NT 6.1; rv:2.0b6pre) Gecko/20100903 Firefox/4.0b6pre Firefox/4.0b6pre';

suite('parse function', function() {
  test('Unexpected args don\'t throw', function() {
    assert.doesNotThrow(function() { parse(USER_AGENT_STRING); });
    assert.doesNotThrow(function() { parse(''); });
    assert.doesNotThrow(function() { parse(); });
    assert.doesNotThrow(function() { parse(null); });
    assert.doesNotThrow(function() { parse({}); });
    assert.doesNotThrow(function() { parse(123); });
  });
});

