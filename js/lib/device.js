exports.Device = Device
function Device(family) {
  this.family = family || 'Other';
}

Device.prototype.toString = function() {
  return this.family;
};


exports.makeParser = function(regexes) {
  var parsers = regexes.map(function (obj) {
    var regexp = new RegExp(obj.regex),
        deviceRep = obj.device_replacement;

    function parser(str) {
      var m = str.match(regexp);
      if (!m) { return null; }

      var family = deviceRep ? deviceRep.replace('$1', m[1]) : m[1];
      return new Device(family);
    }

    return parser;
  });

  function parser(str, ua_family, os_family) {
    var obj;

    if (typeof str === 'string') {
      for (var i = 0, length = parsers.length; i < length; i++) {
        obj = parsers[i](str, ua_family, os_family);
        if (obj) { return obj; }
      }
    }

    return obj || new Device();
  }

  return parser;
};
