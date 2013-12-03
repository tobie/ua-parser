exports.Device = Device;

function Device(family, brand, model, debug) {
  this.family = family || 'Other';
  this.brand  = brand  || null;
  this.model  = model  || null;
  if (debug) { this.debug = debug; }
}

Device.prototype.toString = function() {
  return this.family;
};

function multiReplace(str, m) {
  return str.replace(/\$(\d)/g, function(tmp, i) {
    return m[i] || '';
  }).replace(/\s*$/, '');
}

exports.makeParser = function(regexes) {
  var parsers = regexes.map(function (obj) {
    var regexp = new RegExp(obj.regex),
        deviceRep = obj.device_replacement,
        brandRep  = obj.brand_replacement,
        modelRep  = obj.model_replacement,
        debug     = obj.debug;

    function parser(str) {
      var m = str.match(regexp);
      if (!m) { return null; }

      var family = deviceRep ? multiReplace(deviceRep, m) : m[1];
      var brand  = brandRep  ? multiReplace(brandRep, m)  : null;
      var model  = modelRep  ? multiReplace(modelRep, m)  : m[1];
      return new Device(family, brand, model, debug);
    }

    return parser;
  });

  function parser(str) {
    var obj;

    if (typeof str === 'string') {
      for (var i = 0, length = parsers.length; i < length; i++) {
        obj = parsers[i](str);
        if (obj) { return obj; }
      }
    }

    return obj || new Device();
  }

  return parser;
};
