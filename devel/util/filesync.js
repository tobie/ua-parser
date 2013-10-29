"use strict";

/*!
 * Utility functions to read and write files synchronously
 */

/**
 * Module dependencies
 */
var 
  fs         = require('fs'),
  jsyaml     = require('js-yaml'),
  yamlparser = require('yamlparser');

/**
 * adjust yaml content to fit with yamlparser
 * 
 * `content` in yaml format for test cases gets adjusted. 
 * 
 * @param {String} content
 * @return {String}
 */
function adjustYaml(content) {
  var out = '';
  
  (content.split('\n')).forEach(function(l){
    var m = l.match(/^([^:]+):(.+)$/);
    
    if (m && m[2]) {
      m.shift();
      // preprocessing
      m[1] = m[1]
        .replace(/^\s*"?/, '')
        .replace(/"?\s*$/, '')
        .replace(/^null$/, '')
        .replace(/\\\\x/g, '\\x') // correct bad escaping of \x chars
        .replace(/\\"/g, '"');

      if (!/^\s*$/.test(m[1])) {
        // not empty string
        if (/'/.test(m[1])) {
          // surround with "" if ' found - " needs to be escaped
          m[1] = m[1]
            .replace(/"/, '\\"')
            .replace(/^/, ' "')
            .replace(/$/, '"');
        }
        else { 
          // surround with ''
          m[1] = m[1]
            .replace(/^/, " '")
            .replace(/$/, "'");
        }
      }
      
      if (/^\s*-/.test(m[0])) {
        m[0] = '\n' + m[0];
      }
      
      out += m.join(':') + '\n';
    }
    else {
      out += l + '\n';
    }
  });
  
  return out;
}

/**
 * read and parse yaml file with yamlparser.
 * @param {String} path to yaml file
 * @return {Object} yaml parsed object
 */
function readYamlParserSync(file_path) {
  var content = fs.readFileSync(file_path, 'utf8');
  content = yamlparser.eval(content); // jshint ignore:line
  return content;
}
exports.readYamlParserSync = readYamlParserSync;

/**
 * read and parse yaml file.
 * @param {String} path to yaml file
 * @return {Object} yaml parsed object
 */
function readYamlSync(file_path) {
  var content = fs.readFileSync(file_path, 'utf8');
  content = jsyaml.safeLoad(content);
  return content;
}
exports.readYamlSync = readYamlSync;

/**
 * write yaml file.
 * @param {String} path to yaml file
 * @return {Object} content object
 */
function writeYamlSync(file_path, content) {
  content = adjustYaml(jsyaml.dump(content));
  fs.writeFileSync(file_path, content, 'utf8');
}
exports.writeYamlSync = writeYamlSync;

/**
 * split each line for each user-agent from file
 * file needs to be a text file with one user-agent-string per line
 * @param {String} file
 * @return {Array} List with user-agent-strings
 */
function readUserAgentsListSync(file_path) {
  var user_agents = [];
  var user_agents_list = fs.readFileSync(file_path, 'utf-8');
  user_agents = user_agents_list.split(/[\n]+/);
  // filter agent strings which are less then 2 chars long
  user_agents = user_agents.filter(function(p){
    if (p.length < 2) {
      return false;
    }
    return true;
  });
  return user_agents;
}
exports.readUserAgentsListSync = readUserAgentsListSync;

