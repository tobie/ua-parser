var UNDEF = void 0;

exports.BackwardsCompatResults = BackwardsCompatResults;
function BackwardsCompatResults(ua_str, ua, os, device) {
  this.string = ua_str;
  this.userAgent = ua;
  this.os = os;
  this.device = device

  // Backwards compat
  var major = ua.major,
      minor = ua.minor,
      patch = ua.patch;

  this.family = ua.family;
  this.major = major === null ? UNDEF : parseInt(major);
  this.minor = minor === null ? UNDEF : parseInt(minor);
  this.patch = patch === null ? UNDEF : parseInt(patch);
}

// Backwards compat
BackwardsCompatResults.prototype.toVersionString = function() {
  var output = '',
      ua = this.ua;
  if (ua.major != null) {
    output += ua.major;
    if (ua.minor != null) {
      output += '.' + ua.minor;
      if (ua.patch != null) {
        output += '.' + ua.patch;
      }
    }
  }
  return output;
};

// Backwards compat
BackwardsCompatResults.prototype.toString = function() {
  var suffix = this.toVersionString();
  if (suffix) { suffix = ' ' + suffix; }
  return this.ua.family + suffix;
};

// Backwards compat
BackwardsCompatResults.prototype.toFullString = function() {
  return this.toString() + (this.os ? '/' + this.os : '');
};
