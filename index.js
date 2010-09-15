var path = require('path'), 
    fs = require('fs');

var file = path.join(__dirname, 'regexes.json');
var regexes = fs.readFileSync(file, 'utf8');
regexes = JSON.parse(regexes);

var parsers = regexes.map(function(obj) {
  var regexp = new RegExp(obj.pattern),
      famRep = obj.family_replacement,
      v1Rep = obj.v1_replacement;

  function parser(ua) {
    var m = ua.match(regexp);
    
    if (!m) { return null; }
    
    var familly = famRep ? famRep.replace('$1', m[1]) : m[1];
    
    var obj = new UserAgent(familly);
    obj.major = parseInt(v1Rep ? v1Rep : m[2]);
    obj.minor = m[3] ? parseInt(m[3]) : null;
    obj.patch = m[4] ? parseInt(m[4]) : null;
    
    return obj;
  }
  
  return parser;
});

exports.parse = parse;
function parse(ua) {
  for (var i=0; i < parsers.length; i++) {
    var result = parsers[i](ua);
    if (result) { return result; }
  }
  return new UserAgent();
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

if (require.main === module) {
  var output, input = process.argv[2];
  if (!input) { process.exit(1); }
  process.stdout.write(parse(input).toString());
}
