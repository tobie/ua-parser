var startsWithDigit = require('./helpers').startsWithDigit;

exports.OS = OS
function OS(family, major, minor, patch, patchMinor) {
  this.family = family || 'Other';
  this.major = major || null;
  this.minor = minor || null;
  this.patch = patch || null;
  this.patchMinor = patchMinor || null;
}

OS.prototype.toVersionString = function() {
  var output = '';
  if (this.major != null) {
    output += this.major;
    if (this.minor != null) {
      output += '.' + this.minor;
      if (this.patch != null) {
        if (startsWithDigit(this.patch)) { output += '.'; }
        output += this.patch;
        if (this.patchMinor != null) {
          if (startsWithDigit(this.patchMinor)) { output += '.'; }
          output += this.patchMinor;
        }
      }
    }
  }
  return output;
};

OS.prototype.toString = function() {
  var suffix = this.toVersionString();
  if (suffix) { suffix = ' ' + suffix; }
  return this.family + suffix;
};

function _makeParsers(obj) {
  var regexp = new RegExp(obj.regex),
      famRep = obj.os_replacement,
      majorRep = obj.os_v1_replacement,
      minorRep = obj.os_v2_replacement,
      patchRep = obj.os_v3_replacement,
      patchMinorRep = obj.os_v4_replacement;

  function parser(str) {
    var m = str.match(regexp);
    if (!m) { return null; }
    
    var family = famRep ? famRep.replace('$1', m[1]) : m[1],
        major = majorRep || m[2],
        minor = minorRep || m[3],
        patch = patchRep || m[4],
        patchMinor = patchMinorRep || m[5];

    return new OS(family, major, minor, patch, patchMinor);
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

    return obj || new OS();
  }

  return parser;
}