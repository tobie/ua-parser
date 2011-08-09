var fs = require('fs'),
    path = require('path'),
    http = require('http'),
    yaml = require('yamlparser');
    
exports.YAML_FILE = path.join(__dirname, '..', 'regexes.yaml');
exports.JSON_FILE = path.join(__dirname, '..', 'regexes.json');
exports.SRC_PATH = '/svn/trunk/resources/user_agent_parser.yaml';
exports.SRC_HOST = 'ua-parser.googlecode.com';

exports.fetch = fetch;
function fetch(callback) {
  fs.open(exports.YAML_FILE, 'w+', function(err, fd) {
    if (err) {
      callback(err);
      return;
    }
    var repo = http.createClient(80, exports.SRC_HOST);
    var request = repo.request('GET', exports.SRC_PATH, { host: exports.SRC_HOST });
    request.end();
    request.on('error', callback);
    request.on('response', function (response) {
      var position = 0;
      response.on('data', function (buffer) { 
        fs.write(fd, buffer, 0, buffer.length, position)
        position += buffer.length;
      });
      response.on('end', callback);
    });
  });
}

exports.parse = parse;
function parse(callback) {
  fs.readFile(exports.YAML_FILE, 'utf8', function(err, str) {
    if (err) {
      callback(err);
    } else {
      try {
        callback(null, yaml.eval(str));
      } catch(e) {
        callback(e);
      }
    }
  })
}

exports.dump = dump;
function dump(json, callback) {
  json = JSON.stringify(json, null, 2);
  fs.writeFile(exports.JSON_FILE, json, callback);
}

if (require.main.id === module.id) {
  fetch(function(err) {
    if (err) {
      callback(err);
      return;
    }
    parse(function(err, json) {
      if (err) {
        callback(err);
        return;
      }
      dump(json, function(err) {
        if (err) {
          callback(err);
          return;
        }
        console.log('Fetched, parsed and dumped data into regexes.json.');
      })
    })
  });
}
