"use strict";

var 
  fs       = require('fs'),
  path     = require('path'),
  extend   = require('./extend').extend,  
  fileSync = require('./filesync');

/**
 * 
 */
function makeLists(config, opts) {
  var       
    user_agents, 
    new_tests    = [],
    broken_tests = [],
    broken_log   = [],
    testcase_map = {},
    testcases    = { test_cases: {} },
    attr         = config.attr,
    report       = config.report(opts);

  if (/^[^\.\/]/.test(opts.file)) {
    opts.file = path.normalize(path.join( __dirname, '..', opts.file)); 
  }
  console.log('... reading ' + opts.file);
  user_agents = fileSync.readUserAgentsListSync(opts.file);
  
  if (opts.testcases) {  
    console.log('... reading ' + config.testcasesfile);    
    testcases = fileSync.readYamlSync(config.testcasesfile);
    
    if (opts.appenduas) {
      console.log('... appending user-agents from test cases');
    }
    
    // convert testcases into a hash for faster lookup
    testcases.test_cases.forEach(function(p, index) {
      if (opts.appenduas) {
        user_agents.push(p.user_agent_string);
      }
      testcase_map[p.user_agent_string] = { index: index };
      attr.map(function(pp){
        testcase_map[p.user_agent_string][pp] = p[pp];
      });
    });
  }
  
  console.log('... parsing user-agents');
  // loop over all strings given in the user-agents file
  user_agents.forEach(function(ua_string) {
    var 
      str,
      tmp,
      isBroken,
      ua = config.parser(ua_string);
    
    // adjust camel cases..
    ua.patch_minor = ua.patchMinor || null;

    if (opts.testcases) {  
      // check if testcase is broken
      tmp = testcase_map[ua_string];
      if (tmp) {
        isBroken = false;
        attr.map(function(p){
          if (ua[p] !== tmp[p]) {
            isBroken = true;
          }
        });
        if (isBroken) {
          console.log('Error: broken testcase ' + ua_string);

          str = 
            'user-agent: ' + ua_string  + '\n' +
            '   matcher: ' + ( ua.debug || 'add debug info' ) + '\n';
          
          attr.map(function(p){
            if (ua[p] != tmp[p]) {
              str += 
                p + '\n' +
                '       got: ' + ua[p] + '\n' +
                '  expected: ' + tmp[p] + '\n';
            }
          });
          broken_log.push(str);
          
          // memorize broken user-agent
          broken_tests.push(extend(ua, { index: tmp.index }));
        }
      }
      else {
        ua.user_agent_string = ua_string;
        // add new test case
        new_tests.push(ua);
      }
    }    
    report.add(ua_string, ua);
  });
  
  // change broken test cases
  broken_tests.forEach(function(p){
    var obj = testcases.test_cases[p.index];
    attr.map(function(pp){
      obj[pp] = p[pp];
    });
    testcases.test_cases[p.index] = obj;
  });
  
  // add new test cases
  new_tests.forEach(function(p){

    // create the right object structure (user_agent_string needs to be first)
    var obj = { user_agent_string: p.user_agent_string };
    attr.map(function(pp){
      obj[pp] = p[pp];
    });

    // do not add test cases without any match at the moment
    if (p.family !== 'Other') {
      testcases.test_cases.push(obj);
    }
  });

  // write reports
  console.log('... writing list ' + opts.listfile);
  fs.writeFileSync(opts.listfile, report.show(), 'utf8');

  if (opts.testcases) {
    console.log('... writing broken tests log file ' + opts.logfile);
    fs.writeFileSync(opts.logfile , broken_log.join('----\n'), 'utf8');
    // write test cases
    console.log('... writing testcases file ' + opts.testcasesfile);
    fileSync.writeYamlSync(opts.testcasesfile, testcases);
  }
}

module.exports = makeLists;
