var path = require('path'), 
    fs = require('fs'),
    yaml = require('yamlparser');

var file = path.join(__dirname, '..', 'regexes.yaml');
var regexes = fs.readFileSync(file, 'utf8');
regexes = yaml.eval(regexes);

var mobile_agents = {};
var mobile_user_agent_families = regexes.mobile_user_agent_families.map(function(str) {
  mobile_agents[str] = true;
});

var mobile_os_families = regexes.mobile_os_families.map(function(str) {
  mobile_agents[str] = true;
});

var ua_parsers = regexes.user_agent_parsers.map(function(obj) {
  var regexp = new RegExp(obj.regex),
      famRep = obj.family_replacement,
      majorVersionRep = obj.v1_replacement,
      minorVersionRep = obj.v2_replacement;

  function parser(ua) {
    var m = ua.match(regexp);
    
    if (!m) { return null; }
    
    var family = famRep ? famRep.replace('$1', m[1]) : m[1];
    
    var obj = new UserAgent(family);

    /**
     * Making use of the Double Bitwise Not trick. Reference:
     *  http://www.slideshare.net/madrobby/extreme-javascript-performance
     *  http://james.padolsey.com/javascript/double-bitwise-not/
     *
     * All non-zero equivalents will be truthy and will be floored if float:
     *  ~~true; // 1
     *  ~~4.9;  // 4
     *  ~~(-2.9); // -2
     * null, undefined and false will be falsey:
     * ~~null // 0
     * ~~undefined // 0
     * ~~false // 0
     * For NaN and Infinity, internal ToInt32 converts it to 0.
     * ~~NaN // 0
     * ~~Infinity // 0
     * ~~(1/0) // 0
     */

    obj.major = ~~(majorVersionRep ? majorVersionRep : m[2]);
    obj.minor = ~~(minorVersionRep ? minorVersionRep : m[3]);
    obj.patch = ~~(m[4]);

    if(mobile_agents.hasOwnProperty(family)) {
      obj.isMobile = true;
    }
    if(family == "Spider") {
      obj.isSpider = true;
    }
    
    return obj;
  }
  
  return parser;
});

var os_parsers = regexes.os_parsers.map(function(obj) {
  var regexp = new RegExp(obj.regex),
      osRep  = obj.os_replacement,
      minorVersionRep = obj.os_v1_replacement,
      majorVersionRep = obj.os_v2_replacement;

  function parser(ua) {
    var m = ua.match(regexp);

    if(!m) { return null; }

    var os = {
      family: osRep ? osRep.replace('$1', m[1]) : m[1],
      major : ~~(majorVersionRep ? majorVersionRep : m[2]),
      minor : ~~(minorVersionRep ? minorVersionRep : m[3]),
      patch : ~~(m[4])
    };

    return os;
  }

  return parser;
});

var device_parsers = regexes.device_parsers.map(function(obj) {
  var regexp    = new RegExp(obj.regex),
      deviceRep = obj.device_replacement;

  function parser(ua) {
    var m = ua.match(regexp);

    if(!m) { return null; }

    var device = deviceRep ? deviceRep.replace('$1', m[1]) : m[1];

    return device;
  }

  return parser;
});

exports.parse = parse;
function parse(ua) {
  var result, os, device, i;

  ua_parsers.some(function(u) {
    if(!!(u(ua))) {
      return result = u(ua);
    }
  });

  os_parsers.some(function(o) {
    if(!!(o(ua))) {
      return os = o(ua);
    }
  });

  device_parsers.some(function(d) {
    if(!!(d(ua))) {
      return device = d(ua);
    }
  });

  if(!result) { result = new UserAgent(); }

  if(os) {
    result.os.family = os.family;
    result.os.major  = os.major;
    result.os.minor  = os.minor;
    result.os.patch  = os.patch;
  }
  result.device = device || "Other";
  return result;
}

function toVersionString() {
  var output = '';
  if (this.major) {
    output += this.major;
    if (this.minor) {
      output += '.' + this.minor;
      if (this.patch) {
        output += '.' + this.patch;
      }
    }
  }
  return output;  
}

function toString() {
  var suffix = this.toVersionString();
  if (suffix) { suffix = ' ' + suffix; }
  return this.family + suffix;
}

function OS() {
  this.family = "Other";
  this.major  = 0;
  this.minor  = 0;
  this.patch  = 0;
}

OS.prototype.toVersionString = toVersionString;

OS.prototype.toString = toString;

function UserAgent(family) {
  this.os = new OS();

  this.family = family || 'Other';
  this.major = 0;
  this.minor = 0;
  this.patch = 0;
  this.isMobile = false;
  this.isSpider = false;
}

UserAgent.prototype.toVersionString = toVersionString;

UserAgent.prototype.toString = toString;

UserAgent.prototype.toFullString = function() {
  return this.toString() + (this.os.toString() ? "/" + this.os.toString() : "");
};

if (require.main === module) {
  var output, input = process.argv[2];
  if (!input) { process.exit(1); }
  process.stdout.write(parse(input).toString());
}
