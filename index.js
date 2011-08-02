var path = require('path'), 
    fs = require('fs');

var file = path.join(__dirname, 'regexes.json');
var regexes = fs.readFileSync(file, 'utf8');
regexes = JSON.parse(regexes);

var ua_parsers = regexes.user_agent_parsers.map(function(obj) {
  var regexp = new RegExp(obj.regex),
      famRep = obj.family_replacement,
      majorVersionRep = obj.major_version_replacement;

  function parser(ua) {
    var m = ua.match(regexp);
    
    if (!m) { return null; }
    
    var family = famRep ? famRep.replace('$1', m[1]) : m[1];
    
    var obj = new UserAgent(family);
    obj.major = parseInt(majorVersionRep ? majorVersionRep : m[2]);
    obj.minor = m[3] ? parseInt(m[3]) : null;
    obj.patch = m[4] ? parseInt(m[4]) : null;
    
    return obj;
  }
  
  return parser;
});

var os_parsers = regexes.os_parsers.map(function(obj) {
  var regexp = new RegExp(obj.regex),
      osRep  = obj.os_replacement;

  function parser(ua) {
    var m = ua.match(regexp);

    if(!m) { return null; }

    var os = (osRep ? osRep : m[1]) + (m.length > 2 ? " " + m[2] : "");

    return os;
  }

  return parser;
});

exports.parse = parse;
function parse(ua) {
  var os, i;
  for (i=0; i < ua_parsers.length; i++) {
    var result = ua_parsers[i](ua);
    if (result) { break; }
  }

  for (i=0; i < os_parsers.length; i++) {
    os = os_parsers[i](ua);
    if (os) { break; }
  }

  if(!result) { result = new UserAgent(); }

  result.os = os;
  return result;
}

function UserAgent(family) {
  this.family = family || 'Other';
}

UserAgent.prototype.toVersionString = function() {
  var output = '';
  if (this.major != null) {
    output += this.major;
    if (this.minor != null) {
      output += '.' + this.minor;
      if (this.patch != null) {
        output += '.' + this.patch;
      }
    }
  }
  return output;
};

UserAgent.prototype.toString = function() {
  var suffix = this.toVersionString();
  if (suffix) { suffix = ' ' + suffix; }
  return this.family + suffix;
};

UserAgent.prototype.toFullString = function() {
  return this.toString() + (this.os ? "/" + this.os : "");
};

if (require.main === module) {
  var output, input = process.argv[2];
  if (!input) { process.exit(1); }
  process.stdout.write(parse(input).toString());
}
