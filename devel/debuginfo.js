"use strict";

/*!
 * add or remove debug info to the regexes.yaml file
 * 
 * if debug info is within the regexes.yaml file it is being removed
 * otherwise added.
 */
 
/**
 * Module dependencies
 */
var fs = require('fs'),
    path = require('path');  

/**
 * Module variables
 */
var regexes = path.join(__dirname, '..', 'regexes.yaml');

function addZeros(n, length) {
  var str = '' + n;
  var z = '00000000';
  if (str.length >= length) {
    return str;
  }
  return z.substr(0, length - str.length) + str;
}

function main(args) {
  var data = fs.readFileSync(regexes, 'utf8');
  var cnt = 0;
  var add = true;

  // write a backup - for any cases
  fs.writeFileSync(regexes + '.bak', data, 'utf8');
  
  if (/^\s*debug:\s*'[^']*'/m.test(data)) {
    add = false; 
  }
  
  // delete all debug lines
  data = data.replace(/^\s*debug:\s*'[^']*'[ \t]*\n/mg, '');
  
  // add debug info
  if (add) {
    data = data.replace(/(^[ \t]*)(- regex:\s*'[^']*'[ \t]*)/mg, function(m, m1, m2) {
      return m1 + m2 + "\n" + m1 + "  debug: '#" + addZeros(++cnt, 4) + "'";
    });
    console.log('debug info added to regexes.yaml');
  }
  else {
    console.log('debug info removed from regexes.yaml');
  }
  
  fs.writeFileSync(regexes, data, 'utf8');
}

if (require.main === module) {
  main();
}
