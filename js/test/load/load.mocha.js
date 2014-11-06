/* global suite, test */

// Note: These tests here cannot be executed together with the other tests as mocha does not have a defined execution order

"use strict";

var 
	assert = require('assert'),
  path = require('path'), 
  fs = require('fs'),
  uaParser = require('../../index')(),
  testcasesM = require('./testcases')/*.testcases*/;

var
	file = __dirname + '/../../../regexes.yaml',
	testfile = __dirname + '/load-regexes.yaml',
	testfileWatch = __dirname + '/watch-regexes.yaml',
	testfileNotExists = __dirname + '/does-not-exist.yaml';

function run(testcases) {
	testcases = testcases || testcasesM.testcases;
	testcases.forEach(function(tc){
		test('- ' + tc.string, function(){
			var uaParsed = uaParser.parse(tc.string);
			assert.deepEqual(uaParsed, tc);
			//~ console.log(JSON.stringify(uaParsed));
		});
	});
}

function copySync(src, dest) {
	var content = fs.readFileSync(src, 'utf-8');
	fs.writeFileSync(dest, content, 'utf-8');
}

suite("load regexes", function(){
	
	test('- load custom regexes file', function(){
		var error = uaParser.loadSync({ file: file, backwardsCompatible: false });
		assert.equal(error, null);
		run();
	}); 

	test("- not existing", function(){
		var error = uaParser.loadSync(testfileNotExists);
		assert.ok(/ENOENT/.test(error.message));
	});
	
	test("- async load not existing", function(done){
		uaParser.load(testfileNotExists, function(error){
			assert.ok(/ENOENT/.test(error.message));
			done();
		});
	});

	test("- async load existing", function(done){
		uaParser.load(testfile , function(error){
			assert.equal(error, null);
			run();
			done();
		});
	});
});

suite ("watch tests", function(){
	test("- async watch existing", function(done){
		this.timeout(10000);

		copySync(file, testfileWatch);
		run(testcasesM.testcasesRegexes);
		
		uaParser.watch(testfileWatch , function(error){
			fs.unwatchFile(testfileWatch);
			assert.equal(error, null);
			run();
			done();
		});
		
		setTimeout(function(){
			copySync(testfile, testfileWatch);
		}, 10);
		
	});
});
