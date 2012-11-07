var path = require('path'), 
    fs = require('fs'),
    yaml = require('yamlparser');

var file = path.join(__dirname, '..', 'regexes.yaml');
var regexes = fs.readFileSync(file, 'utf8');
regexes = yaml.eval(regexes);

var mobile_agents = {};

for(var i in regexes.mobile_user_agent_families) {
  mobile_agents[regexes.mobile_user_agent_families[i]] = true;
}
for(var i in regexes.mobile_os_families) {
  mobile_agents[regexes.mobile_os_families[i]] = true;
}

var ua_parsers = regexes.user_agent_parsers.map(function(obj) {
  var regexp = new RegExp(obj.regex),
      famRep = obj.family_replacement,
      majorVersionRep = obj.v1_replacement,
      minorVersionRep = obj.v2_replacement;

  function parser(ua) {
    if(!ua || !ua.length) {
      throw new Error('UserAgent not specified');
    }

    var m = ua.match(regexp);
    
    if (!m) { return null; }
    
    var family = famRep ? famRep.replace('$1', m[1]) : m[1];
    
    var obj = new UserAgent(family);

    obj.major = (majorVersionRep ? majorVersionRep : m[2]) || '';
    obj.minor = (minorVersionRep ? minorVersionRep : m[3]) || '';
    obj.patch = m[4] || '';
    obj.majorInt = ~~parseInt(obj.major);
    obj.minorInt = ~~parseInt(obj.minor);
    obj.patchInt = ~~parseInt(obj.patch);

    if(mobile_agents.hasOwnProperty(family)) {
      obj.device.isMobile = true;
    }
    if(family == "Spider") {
      obj.device.isSpider = true;
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
      family: (osRep ? osRep.replace('$1', m[1]) : m[1]) || '',
      major : (majorVersionRep ? majorVersionRep : m[2]) || '',
      minor : (minorVersionRep ? minorVersionRep : m[3]) || '',
      patch : m[4] || ''
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
    return result = u(ua);
  });

  os_parsers.some(function(o) {
    return os = o(ua);
  });

  device_parsers.some(function(d) {
    return device = d(ua);
  });

  if(!result) { result = new UserAgent(); }

  if(os) {
    result.os.family = os.family;
    result.os.major  = os.major;
    result.os.minor  = os.minor;
    result.os.patch  = os.patch;
    result.os.majorInt = ~~parseInt(os.major);
    result.os.minorInt = ~~parseInt(os.minor);
    result.os.patchInt = ~~parseInt(os.patch);
  }
  result.device.family = device || "Other";
  return result;
}

function toVersionString() {
  var output = '';
  if (this.major.length) {
    output += this.major;
    if (this.minor.length) {
      output += '.' + this.minor;
      if (this.patch.length) {
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
  this.family = 'Other';
  this.major  = '';
  this.minor  = '';
  this.patch  = '';
  this.majorInt  = 0;
  this.minorInt  = 0;
  this.patchInt  = 0;
}

OS.prototype.toVersionString = toVersionString;

OS.prototype.toString = toString;

function UserAgent(family) {
  this.os = new OS();

  this.family = family || 'Other';
  this.major = '';
  this.minor = '';
  this.patch = '';
  this.majorInt = 0;
  this.minorInt = 0;
  this.patchInt = 0;
  this.device = {
    isMobile: false,
    isSpider: false,
    family  : 'Other'
  }
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