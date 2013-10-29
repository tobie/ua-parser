"use strict";

/*!
 * debug os_parser regular expressions
 *
 * Note: The yamlparser stops parsing strings which contain unicode
 *       characters. To parse and dump the testcases js-yaml is used 
 *       instead.
 */

/**
 * Dependencies
 */
var 
  fs        = require('fs'),
  path      = require('path'),
  osParser  = require('../js/lib/os'),
  argvParse = require('./util/arguments').parse,
  fileSync  = require('./util/filesync'),
  makeLists = require('./util/makelists'),
  extend    = require('./util/extend').extend,
  report    = require('./util/report');

/**
 * Variables
 */
var options = {
  file:          "useragents.txt",    // user-agents file
  testcasesfile: "test_user_agent_parser_os.yaml",  // output file for test cases
  listfile:      "os.csv",            // output file os list
  logfile:       "os.log",            // output logfile for broken test cases
  console: false,     // output parsing matches on console
  family: true,       // show family info in output
  debug: true,        // show debug info (Note: use debuginfo.js to 
                      // add them in regexes.yaml first)
  swapdebug: false,   // show debug info after column brand model to 
                      //check matches for specific devices
  testcases: false,  // process testcases as well, outputs
  appenduas: true    // append user-agents from test-cases
};

var 
  regexesfile     = path.join(__dirname, '..', 'regexes.yaml'),
  testcasesfile   = path.join(__dirname, '..', 'test_resources', options.testcasesfile);

// add dirname to all output files - may be overwritten on cli
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
  regexes = fileSync.readYamlSync(regexesfile);
  parser = osParser.makeParser(regexes.os_parsers);

  makeLists({ 
    attr  : ['family', 'major', 'minor', 'patch', 'patch_minor'],
    parser: parser,
    report: report.ua,
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
      '  node os.js file:uas.txt testcases:true'
    );
  }
}

