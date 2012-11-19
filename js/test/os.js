var assert = require('assert'),
    OS = require('../lib/os').OS,
    makeParser = require('../lib/os').makeParser;

suite('os object', function() {
  test('OS constructor with no arguments', function() {
    var os = new OS();
    assert.strictEqual(os.family, 'Other');
    assert.strictEqual(os.major, null);
    assert.strictEqual(os.minor, null);
    assert.strictEqual(os.patch, null);
    assert.strictEqual(os.patchMinor, null);
  });

  test('OS constructor with valid arguments', function() {
    var os = new OS('Bar', '4', '3', '2', '1');
    assert.strictEqual(os.family, 'Bar');
    assert.strictEqual(os.major, '4');
    assert.strictEqual(os.minor, '3');
    assert.strictEqual(os.patch, '2');
    assert.strictEqual(os.patchMinor, '1');
  });

  test('OS#toVersionString with only numerical args', function() {
    assert.strictEqual(new OS('Bar', '4', '3', '2', '1').toVersionString(), '4.3.2.1');
    assert.strictEqual(new OS('Bar', '4', '3', '2').toVersionString(), '4.3.2');
    assert.strictEqual(new OS('Bar', '4', '3').toVersionString(), '4.3');
    assert.strictEqual(new OS('Bar', '4').toVersionString(), '4');
    assert.strictEqual(new OS('Bar').toVersionString(), '');
  });

  test('OS#toVersionString with non numerical args', function() {
    assert.strictEqual(new OS('Bar', '4', '3', '2', 'beta').toVersionString(), '4.3.2beta');
    assert.strictEqual(new OS('Bar', '4', '3', 'beta').toVersionString(), '4.3beta');
  });

  test('OS#toString for known OS', function() {
    assert.strictEqual(new OS('Bar', '4', '3', '2', '1').toString(), 'Bar 4.3.2.1');
  });

  test('OS#toString for unknown OS', function() {
    assert.strictEqual(new OS().toString(), 'Other');
  });
});

suite('OS parser', function() {
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

  test('Parser returns an instance of OS when unsuccessful at parsing', function() {
    var parse = makeParser([]);
    assert.ok(parse('foo') instanceof OS);
  });

  test('Parser returns an instance of OS when sucessful', function() {
    var parse = makeParser([{regex: 'foo'}]);
    assert.ok(parse('foo') instanceof OS);
  });

  test('Parser correctly identifies OS name', function() {
    var parse = makeParser([{regex: '(foo)'}]);
    assert.strictEqual(parse('foo').family, 'foo');
  });

  test('Parser correctly identifies version numbers', function() {
    var parse = makeParser([{regex: '(foo) (\\d)\\.(\\d).(\\d)\\.(\\d)'}]),
        os = parse('foo 1.2.3.4');
    assert.strictEqual(os.family, 'foo');
    assert.strictEqual(os.major, '1');
    assert.strictEqual(os.minor, '2');
    assert.strictEqual(os.patch, '3');
    assert.strictEqual(os.patchMinor, '4');
  });

  test('Parser correctly processes replacements', function() {
    var parse = makeParser([{
      regex: '(foo) (\\d)\\.(\\d)\\.(\\d)\\.(\\d)',
      os_replacement: '$1bar',
      os_v1_replacement: 'a',
      os_v2_replacement: 'b',
      os_v3_replacement: 'c',
      os_v4_replacement: 'd'
    }]);

    var os = parse('foo 1.2.3.4');
    assert.strictEqual(os.family, 'foobar');
    assert.strictEqual(os.major, 'a');
    assert.strictEqual(os.minor, 'b');
    assert.strictEqual(os.patch, 'c');
  });
});

