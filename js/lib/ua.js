var startsWithDigit = require('./helpers').startsWithDigit,
    OS = require('./os').OS;

exports.UA = UA
function UA(family, major, minor, patch) {
  this.family = family || 'Other';
  this.major = major || null;
  this.minor = minor || null;
  this.patch = patch || null;
}

require('util').inherits(UA, OS)

function _makeParsers(obj) {
  var regexp = new RegExp(obj.regex),
      famRep = obj.family_replacement,
      majorRep = obj.v1_replacement,
      minorRep = obj.v2_replacement,
      patchRep = obj.v3_replacement;

  function parser(str) {
    var m = str.match(regexp);
    if (!m) { return null; }
    
    var family = famRep ? famRep.replace('$1', m[1]) : m[1],
        major = majorRep || m[2],
        minor = minorRep || m[3],
        patch = patchRep || m[4];
    
    return new UA(family, major, minor, patch);
  }

  return parser;
}

exports.makeParser = function(regexes) {
  var parsers = regexes.map(_makeParsers)

  function parser(str) {
    var obj;

    if (typeof str === 'string') {
      for (var i = 0, length = parsers.length; i < length; i++) {
        obj = parsers[i](str);
        if (obj) { return obj; }
      }
    }
    
    return obj || new UA();
  }

  return parser;
}