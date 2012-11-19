var assert = require('assert'),
    Device = require('../lib/device').Device,
    UA = require('../lib/ua').UA,
    OS = require('../lib/os').OS,
    makeParser = require('../lib/device').makeParser;

suite('Device object', function() {
  test('Device constructor with no arguments', function() {
    var device = new Device();
    assert.strictEqual(device.family, 'Other');
    assert.strictEqual(device.toString(), 'Other');
    assert.strictEqual(device.isMobile, false);
    assert.strictEqual(device.isSpider, false);
  });

  test('Device constructor with valid arguments', function() {
    var device = new Device('Foo', true, false);
    assert.strictEqual(device.family, 'Foo');
    assert.strictEqual(device.toString(), 'Foo');
    assert.strictEqual(device.isMobile, true);
    assert.strictEqual(device.isSpider, false);
  });
  
  test('Device constructor with truthy and falsy arguments', function() {
    var device = new Device('Foo', 0, 1);
    assert.strictEqual(device.family, 'Foo');
    assert.strictEqual(device.toString(), 'Foo');
    assert.strictEqual(device.isMobile, false);
    assert.strictEqual(device.isSpider, true);
  });
});

var parser = {
  parseOS: function() { return new OS('webOS') },
  parseUA: function() { return new UA('Safari Mobile') }
};

suite('Device parser', function() {
  test('makeParser returns a function', function() {
    assert.equal(typeof makeParser([], null, null, parser), 'function');
  });

  test('Unexpected args don\'t throw', function() {
    var parse = makeParser([], null, null, parser);
    assert.doesNotThrow(function() { parse('Foo'); });
    assert.doesNotThrow(function() { parse(''); });
    assert.doesNotThrow(function() { parse(); });
    assert.doesNotThrow(function() { parse(null); });
    assert.doesNotThrow(function() { parse({}); });
    assert.doesNotThrow(function() { parse(123); });
  });

  test('Parser returns an instance of Device when unsuccessful at parsing', function() {
    var parse = makeParser([{regex: 'foo'}], null, null, parser);
    assert.ok(parse('bar') instanceof Device);
  });

  test('Parser returns an instance of Device when sucessful', function() {
    var parse = makeParser([{regex: 'foo'}], null, null, parser);
    assert.ok(parse('foo') instanceof Device);
  });

  test('Parser correctly identifies Device name', function() {
    var parse = makeParser([{regex: '(foo)'}], null, null, parser);
    assert.strictEqual(parse('foo').family, 'foo');
  });
  
  test('Parser correctly identifies whether a device is mobile', function() {
    var parse = makeParser([{regex: '(foo)'}], ['Safari Mobile'], ['webOS'], parser);
    assert.strictEqual(parse('foo', 'Safari Mobile', 'iPad').isMobile, true);
    assert.strictEqual(parse('foo', 'Opera mini', 'webOS').isMobile, true);
    assert.strictEqual(parse('foo', 'Opera', 'Ubuntu').isMobile, false);
  });
  
  test('Parser correctly identifies whether a device is mobile, even when not provided with the ua and os family names', function() {

    var parse = makeParser([{regex: '(foo)'}], ['Safari Mobile'], null, parser);
    assert.strictEqual(parse('foo').isMobile, true);
    parse = makeParser([{regex: '(foo)'}], null, ['webOS'], parser);
    assert.strictEqual(parse('foo').isMobile, true);
    parse = makeParser([{regex: '(foo)'}], null, null, parser);
    assert.strictEqual(parse('foo').isMobile, false);
  });


  test('Parser correctly identifies whether a device is a bot', function() {

    var parse = makeParser([{
      regex: '(bot)',
      device_replacement: 'Spider'
    }], null, null, parser);
    
    assert.strictEqual(parse('bot').isSpider, true);
    parse = makeParser([{regex: '(foo)'}], null, null, parser);
    assert.strictEqual(parse('foo').isSpider, false);
  });

  test('Parser correctly processes replacements', function() {
    var parse = makeParser([{
      regex: '(foo)',
      device_replacement: '$1bar'
    }], null, null, parser);
  
    var device = parse('foo');
    assert.strictEqual(device.family, 'foobar');
  });
});

