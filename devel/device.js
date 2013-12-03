"use strict";

/*!
 * debug device_parsers regular expressions
 *
 * Note: The yamlparser stops parsing strings which contain unicode
 *       characters. To parse and dump the testcases js-yaml is used 
 *       instead.
 */

/**
 * Dependencies
 */
var 
  fs           = require('fs'),
  path         = require('path'),
  deviceParser = require('../js/lib/device'),
  argvParse    = require('./util/arguments').parse,
  fileSync     = require('./util/filesync'),
  makeLists    = require('./util/makelists'),
  extend       = require('./util/extend').extend,
  report       = require('./util/report');

/**
 * Variables
 */
var options = {
  file:          "useragents.txt",    // user-agents file
  testcasesfile: "test_device.yaml",  // output file for test cases
  listfile:      "device.csv",        // output file device list
  logfile:       "device.log",        // output logfile for broken test cases
  console: false,     // output parsing matches on console
  family: true,       // show family info in output
  debug: true,        // show debug info (Note: use debuginfo.js to 
                      // add them in regexes.yaml first)
  swapdebug: false,   // show debug info after column brand model to 
                      // check matches for specific devices
  testcases: false,   // process testcases as well, outputs
  appenduas: true     // append user-agents from test-cases
};

var
  regexesfile     = path.join(__dirname, '..', 'regexes.yaml'),
  testcasesfile   = path.join(__dirname, '..', 'test_resources', options.testcasesfile);

// add dirname to all output files
(['testcasesfile', 'listfile', 'logfile']).map(function(p){
  options[p] = path.join(__dirname, options[p]);
});


/**
 * 
 */
function main(opts) {
  var 
    parser,
    regexes;

  opts = extend(options, opts);

  console.log('... reading ' + regexesfile);    
  regexes = fileSync.readYamlParserSync(regexesfile);
  parser = deviceParser.makeParser(regexes.device_parsers);

  makeLists({ 
    attr  : ['family', 'brand', 'model'],
    parser: parser,
    report: report.device,
    testcasesfile : testcasesfile
  }, opts);
}

/**
 * command line interface
 */
if (require.main === module) {
  var argv,
      opts;
  
  argv = process.argv.slice(0);
  
  opts = argvParse(argv);
  if (opts) {
    main(opts);
  }
  else {
    console.log( 
      'Error: Could not parse arguments into JSON\n' +
      'Usage: (e.g.)\n' + 
      '  node device.js file:uas.txt family:false testcases:true'
    );
  }
}

