var assert = require('assert'),
    Device = require('../lib/device').Device,
    makeParser = require('../lib/device').makeParser;

suite('Device object', function() {
  test('Device constructor with no arguments', function() {
    var device = new Device();
    assert.strictEqual(device.family, 'Other');
    assert.strictEqual(device.toString(), 'Other');
  });

  test('Device constructor with valid arguments', function() {
    var device = new Device('Foo');
    assert.strictEqual(device.family, 'Foo');
    assert.strictEqual(device.toString(), 'Foo');
  });
});

suite('Device parser', function() {
  test('makeParser returns a function', function() {
    assert.equal(typeof makeParser([]), 'function');
  });

  test('Unexpected args don\'t throw', function() {
    var parse = makeParser([]);
    assert.doesNotThrow(function() { parse('Foo'); });
    assert.doesNotThrow(function() { parse(''); });
    assert.doesNotThrow(function() { parse(); });
    assert.doesNotThrow(function() { parse(null); });
    assert.doesNotThrow(function() { parse({}); });
    assert.doesNotThrow(function() { parse(123); });
  });

  test('Parser returns an instance of Device when unsuccessful at parsing', function() {
    var parse = makeParser([{regex: 'foo'}]);
    assert.ok(parse('bar') instanceof Device);
  });

  test('Parser returns an instance of Device when sucessful', function() {
    var parse = makeParser([{regex: 'foo'}]);
    assert.ok(parse('foo') instanceof Device);
  });

  test('Parser correctly identifies Device name', function() {
    var parse = makeParser([{regex: '(foo)'}]);
    assert.strictEqual(parse('foo').family, 'foo');
  });

  test('Parser correctly processes replacements', function() {
    var parse = makeParser([{
      regex: '(foo)',
      device_replacement: '$1bar'
    }]);
  
    var device = parse('foo');
    assert.strictEqual(device.family, 'foobar');
  });
});

