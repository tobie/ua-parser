var assert = require('assert'),
    UA = require('../lib/ua').UA,
    makeParser = require('../lib/ua').makeParser;

suite('UA object', function() {
  test('UA constructor with no arguments', function() {
    var ua = new UA();
    assert.strictEqual(ua.family, 'Other');
    assert.strictEqual(ua.major, null);
    assert.strictEqual(ua.minor, null);
    assert.strictEqual(ua.patch, null);
  });

  test('UA constructor with valid arguments', function() {
    var ua = new UA('Firefox', '16', '3', 'beta');
    assert.strictEqual(ua.family, 'Firefox');
    assert.strictEqual(ua.major, '16');
    assert.strictEqual(ua.minor, '3');
    assert.strictEqual(ua.patch, 'beta');
  });

  test('UA#toVersionString with only numerical args', function() {
    assert.strictEqual(new UA('Firefox', '16', '3', '2').toVersionString(), '16.3.2');
  });

  test('UA#toVersionString with non numerical patch version', function() {
    assert.strictEqual(new UA('Firefox', '16', '3', 'beta').toVersionString(), '16.3beta');
  });

  test('UA#toString for known UA', function() {
    assert.strictEqual(new UA('Firefox', '16', '3', '2').toString(), 'Firefox 16.3.2');
  });

  test('UA#toString for unknown UA', function() {
    assert.strictEqual(new UA().toString(), 'Other');
  });
});


suite('UA parser', function() {
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

  test('Parser returns an instance of UA when unsuccessful at parsing', function() {
    assert.ok(makeParser([])('bar') instanceof UA);
  });

  test('Parser returns an instance of UA when sucessful', function() {
    var parse = makeParser([{regex: 'foo'}]);
    assert.ok(parse('foo') instanceof UA);
  });

  test('Parser correctly identifies UA name', function() {
    var parse = makeParser([{regex: '(foo)'}]);
    assert.strictEqual(parse('foo').family, 'foo');
  });

  test('Parser correctly identifies version numbers', function() {
    var parse = makeParser([{regex: '(foo) (\\d)\\.(\\d)\\.(\\d)'}]),
        ua = parse('foo 1.2.3');
    assert.strictEqual(ua.family, 'foo');
    assert.strictEqual(ua.major, '1');
    assert.strictEqual(ua.minor, '2');
    assert.strictEqual(ua.patch, '3');
  });

  test('Parser correctly processes replacements', function() {
    var parse = makeParser([{
      regex: '(foo) (\\d)\\.(\\d).(\\d)',
      family_replacement: '$1bar',
      v1_replacement: 'a',
      v2_replacement: 'b',
      v3_replacement: 'c'
    }]);
  
    var ua = parse('foo 1.2.3');
    assert.strictEqual(ua.family, 'foobar');
    assert.strictEqual(ua.major, 'a');
    assert.strictEqual(ua.minor, 'b');
    assert.strictEqual(ua.patch, 'c');
  });
});

