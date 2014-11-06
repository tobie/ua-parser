"use strict";

// testcases running with ./load-regexes.yaml
exports.testcases = [ 
  {"ua":{"family":"TEST Opera Mobile","major":"15","minor":"0","patch":"1162"},"os":{"family":"TEST Android","major":"4","minor":"2","patch":"2","patchMinor":null},"device":{"family":"Other","brand":null,"model":null},"string":"Mozilla/5.0 (Linux; Android 4.2.2; SAMSUNG-SGH-I537 Build/JDQ39) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.63 Mobile Safari/537.36 OPR/15.0.1162.60140"},
	{"ua":{"family":"Other","major":null,"minor":null,"patch":null},"os":{"family":"TEST Android","major":"4","minor":"3","patch":"1","patchMinor":null},"device":{"family":"Other","brand":null,"model":null},"string":"Mozilla/5.0 (Linux; Android 4.3.1; en-US; SAMSUNG GT-I9300 Build/JSS15J) AppleWebKit/537.36 (KHTML, like Gecko) Version/1.5 Chrome/28.0.1500.94 Mobile Safari/537.36"},
	{"ua":{"family":"TEST Opera Mobile","major":"19","minor":"0","patch":"1340"},"os":{"family":"TEST Android","major":"4","minor":"1","patch":"2","patchMinor":null},"device":{"family":"Other","brand":null,"model":null},"string":"Mozilla/5.0 (Linux; Android 4.1.2; HTC Desire 600 dual sim Build/JZO54K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.55 Mobile Safari/537.36 OPR/19.0.1340.68031"}
];

// testcases running with ../../../regexes.yaml
exports.testcasesRegexes = [
	{"ua":{"family":"Opera Mobile","major":"15","minor":"0","patch":"1162"},"os":{"family":"Android","major":"4","minor":"2","patch":"2","patchMinor":null},"device":{"family":"SAMSUNG-SGH-I537","brand":"Samsung","model":"SGH-I537"},"string":"Mozilla/5.0 (Linux; Android 4.2.2; SAMSUNG-SGH-I537 Build/JDQ39) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.63 Mobile Safari/537.36 OPR/15.0.1162.60140"},
	{"ua":{"family":"Chrome Mobile","major":"28","minor":"0","patch":"1500"},"os":{"family":"Android","major":"4","minor":"3","patch":"1","patchMinor":null},"device":{"family":"SAMSUNG GT-I9300","brand":"Samsung","model":"GT-I9300"},"string":"Mozilla/5.0 (Linux; Android 4.3.1; en-US; SAMSUNG GT-I9300 Build/JSS15J) AppleWebKit/537.36 (KHTML, like Gecko) Version/1.5 Chrome/28.0.1500.94 Mobile Safari/537.36"},
	{"ua":{"family":"Opera Mobile","major":"19","minor":"0","patch":"1340"},"os":{"family":"Android","major":"4","minor":"1","patch":"2","patchMinor":null},"device":{"family":"HTC Desire 600 dual sim","brand":"HTC","model":"Desire 600 dual sim"},"string":"Mozilla/5.0 (Linux; Android 4.1.2; HTC Desire 600 dual sim Build/JZO54K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.55 Mobile Safari/537.36 OPR/19.0.1340.68031"}
];
