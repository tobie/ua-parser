var assert = require('assert'),
    helpers = require('../lib/helpers');

suite('Helpers', function() {
  test('startsWithDigit', function() {
    assert.ok(helpers.startsWithDigit('0'));
    assert.ok(helpers.startsWithDigit('1'));
    assert.ok(helpers.startsWithDigit('0a'));
    assert.ok(!helpers.startsWithDigit('a'));
  });
});

