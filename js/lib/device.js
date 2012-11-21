exports.Device = Device
function Device(family, isMobile, isTablet, isSpider) {
  this.family = family || 'Other';
  this.isMobile = !!isMobile;
  this.isTablet = !!isTablet;
  this.isSpider = !!isSpider;
}

Device.prototype.toString = function() {
  return this.family;
};


exports.makeParser = function(regexes, mobile_ua_families, mobile_os_families, tablet_families, mainModule) {
  var parsers = regexes.map(function (obj) {
    mobile_ua_families = mobile_ua_families || [];
    mobile_os_families = mobile_os_families || [];
    tablet_families = tablet_families || [];

    var regexp = new RegExp(obj.regex),
        deviceRep = obj.device_replacement;

    function parser(str, ua_family, os_family) {
      var m = str.match(regexp);
      if (!m) { return null; }

      var family = deviceRep ? deviceRep.replace('$1', m[1]) : m[1],
          isMobile = false,
          isTablet = false,
          isSpider = family === 'Spider';

      if (!ua_family) {
        ua_family = mainModule.parseUA(str).family;
      }

      if (!os_family) {
        os_family = mainModule.parseOS(str).family;
      }

      for (var i = 0, length = mobile_ua_families.length; i < length; i++) {
        if (ua_family.indexOf(mobile_ua_families[i]) > -1) {
          isMobile = true;
          break;
        }
      }

      for (var i = 0, length = mobile_os_families.length; i < length; i++) {
        if (os_family.indexOf(mobile_os_families[i]) > -1) {
          isMobile = true;
          break;
        }
      }

      for (var i = 0, length = tablet_families.length; i < length; i++) {
        if (family.indexOf(tablet_families[i]) > -1) {
          isTablet = true;
          isMobile = false;
          break;
        }
      }
      return new Device(family, isMobile, isTablet, isSpider);
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
