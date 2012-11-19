exports.Device = Device
function Device(family, isMobile, isSpider) {
  this.family = family || 'Other';
  this.isMobile = !!isMobile;
  this.isSpider = !!isSpider;
}

Device.prototype.toString = function() {
  return this.family;
};


exports.makeParser = function(regexes, mobile_ua_families, mobile_os_families, mainModule) {
  var parsers = regexes.map(function (obj) {
    var regexp = new RegExp(obj.regex),
        deviceRep = obj.device_replacement;

    function parser(str, ua_family, os_family) {
      var m = str.match(regexp);
      if (!m) { return null; }

      var family = deviceRep ? deviceRep.replace('$1', m[1]) : m[1],
          isMobile = false,
          isSpider = family === 'Spider';

      if (!ua_family) {
        ua_family = mainModule.parseUA(str).family;
      }

      if (!os_family) {
        os_family = mainModule.parseOS(str).family;
      }

      if ((mobile_ua_families && mobile_ua_families.indexOf(ua_family) > -1)
        || (mobile_os_families && mobile_os_families.indexOf(os_family) > -1)) {
        isMobile = true;
      }

      return new Device(family, isMobile, isSpider);
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
