"use strict";

var path = require('path'),
		fs = require('fs'),
		yaml = require('yamlparser'),
		Results = require('./lib/results').BackwardsCompatResults;

/**
 * ua-parser
 * 
 * @param {Object|string} options - (optional) if "undefined" than default file is choosen. If string is given than that file is used.
 * @property {string} options.file - filename used as regexes file.
 * @property {Boolean} options.backwardsCompatible - set "true" if backwardsCompatible interface is desired
 */ 
module.exports = function(options) {

	var
		uaParser = {},
		dummy = function(){}, // prevent exception if parsers are not yet fully loaded
		parseUA = dummy,
		parseOS = dummy,
		parseDevice = dummy,
		isParsers = false, // flag to indicate availability of parsers
		config = {
			async: false,
			backwardsCompatible: false,
			file: path.join(__dirname, '..', 'regexes.yaml'),
		};

	/**
	 * Parse the User-Agent string `str` for User-Agent
	 *
	 * @param {string} str - Browsers User-Agent string
	 * @return {Object} - { family:, major:, minor:, patch: }
	 */
	uaParser.parseUA = function (str) {
		return parseUA(str);
	};

	/**
	 * Parse the User-Agent string `str` for Operating System
	 *
	 * @param {string} str - Browsers User-Agent string
	 * @return {Object} - { family:, major:, minor:, patch:, patchMinor: }
	 */
	uaParser.parseOS = function (str) {
		return parseOS(str);
	};

	/**
	 * Parse the User-Agent string `str` for Device
	 *
	 * @param {string} str - Browsers User-Agent string
	 * @return {Object} { family:, brand:, model: }
	 */
	uaParser.parseDevice = function (str) {
		return parseDevice(str);
	};

	/**
	 * Parse the User-Agent string `str` for User-Agent, Operating System, Device
	 *
	 * @param {string} str - Browsers User-Agent string
	 * @return {Object} - { family:, brand:, model: }
	 */
	uaParser.parse = function (str) {

		var
			ua = uaParser.parseUA(str),
			os = uaParser.parseOS(str),
			device = uaParser.parseDevice(str);

		if (!config.backwardsCompatible) {
			return { ua: ua, os: os, device: device, string: str };
		}
		else if (ua) {
			return new Results(str, ua, os, device);
		}
	};

	/*
	 * Create the different parsers
	 * @private
	 * @param {Object} regexes - The regexes pattern objects
	 */
	var createParsers = function (regexes) {
		var error = null;

		isParsers = false;

		if (regexes) {

			var 
				_parseUA = require('./lib/ua').makeParser(regexes.user_agent_parsers),
				_parseOS = require('./lib/os').makeParser(regexes.os_parsers),
				_parseDevice = require('./lib/device').makeParser(regexes.device_parsers);

			// assign parsers only if everything went ok
			if (_parseUA && _parseOS && _parseDevice) {
				parseUA     = _parseUA;
				parseOS     = _parseOS;
				parseDevice = _parseDevice;
				isParsers   = true;
			}
			else {
				error = new Error('could not make parsers');
			}
		}
		else {
			error = new Error('bad regexes');
		}

		return error;
	};

	/*
	 * Set some options persistently
	 * @private
	 * @param {Object} options
	 */
	var setOptions = function (options) {
		var i; 
		
		options = options || config;
		options.file = options.file || config.file;

		for (i in config) {
			if (i !== "file" && options[i]) {
				config[i] = options[i];
			}
		}

		return options;
	};

	/**
	 * Synchronously load the ua-parsers regexes file
	 *
	 * @param {Object|string} options - (optional) if "undefined" than default file is choosen. If string is given than that file is used.
	 * @property {string} options.file - filename used as regexes file.
	 * @property {Boolean} options.backwardsCompatible - set "true" if backwardsCompatible interface is desired
	 * @return {Boolean} true if file was loaded otherwise false.
	 */
	uaParser.loadSync = function (options) {
		var
			regexes,
			contents,
			error = null;

		if (typeof options === 'string') {
			options = { file: options };
		}

		options = setOptions(options);

		if (fs.existsSync(options.file)) {
			contents = fs.readFileSync(options.file, 'utf8');

			if (contents) {
				regexes = yaml.eval(contents); // jshint ignore:line
				error = createParsers(regexes);
			}
			else {
				error = new Error('no content found in ' + options.file);
			}
		}
		else {
			error = new Error('ENOENT, open \'' + options.file + '\'');
		}

		return error;
	};

	/**
	 * Asynchronously load the ua-parsers regexes file
	 *
	 * @param {Object|string} options - (optional) if "undefined" than default file is choosen. If string is given than that file is used.
	 * @property {string} options.file - filename used as regexes file.
	 * @property {Boolean} options.backwardsCompatible - set "true" if backwardsCompatible interface is desired
	 * @param {Function} callback - callback(error)
	 */
	uaParser.load = function (options, callback/*(error)*/) {

		if (typeof options === 'string') {
			options = { file: options };
		}
		else if (typeof options === 'function') {
			callback = options;
			options = {};
		}

		options = setOptions(options);

		fs.readFile(options.file, 'utf8', function (error, contents){

			if (!error && contents) {
				var regexes = yaml.eval(contents); // jshint ignore:line
				error = createParsers(regexes);
			}

			if (callback) callback(error);
		});
	};

	/**
	 * Watch a regexes file and reload if there are any changes
	 *
	 * @param {Object|string} options - (optional) if "undefined" than default file is choosen. If string is given than that file is used.
	 * @property {string} options.file - filename used as regexes file.
	 * @property {Boolean} options.backwardsCompatible - set "true" if backwardsCompatible interface is desired
	 * @param {Function} callback - callback(error)
	 */
	uaParser.watch = function (options, callback/*(error)*/) {

		if (typeof options === 'string') {
			options = { file: options };
		}
		else if (typeof options === 'function') {
			callback = options;
			options = {};
		}

		options = setOptions(options);

		fs.watchFile(options.file, function (curr, prev){
			if (curr.mtime === prev.mtime) {
				if (callback) callback(null);
				return;
			}
			if (callback) uaParser.load(options, callback);
		});
	};

	options = setOptions(options);
	if (!config.async) {
		uaParser.loadSync(options);
	}
	
	return uaParser;
};
