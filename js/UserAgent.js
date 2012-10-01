(function (global) {
	var
		/**
		 * @var {Function[]} List of functions to parse the browser family from the UA.
		 */
		ua_parsers = [],
		/**
		 * @var {Function[]} List of functions to parse the operating system from the UA.
		 */
		os_parsers = [];

	/**
	 * @constructor
	 */
	function UserAgent(family) {
		this.family = family || 'Other';
	}

	/**
	 * @static
	 * @param {Object} regexes The parsed result of regexes.yaml.
	 */
	UserAgent.setupRegexes = function setParsers(regexes) {
		if (regexes.user_agent_parsers) {
			ua_parsers = regexes.user_agent_parsers.map(function (obj) {
				var regexp = new RegExp(obj.regex),
					famRep = obj.family_replacement,
					majorVersionRep = obj.v1_replacement;

				function parser(ua) {
					var m, family, obj;
					m = ua.match(regexp);

					if (!m) {
						return null;
					}

					family = famRep ? famRep.replace('$1', m[1]) : m[1];

					obj = new UserAgent(family);
					obj.major = parseInt(majorVersionRep ? majorVersionRep : m[2], 10);
					obj.minor = m[3] ? parseInt(m[3], 10) : null;
					obj.patch = m[4] ? parseInt(m[4], 10) : null;

					return obj;
				}

				return parser;
			});
		}

		if (regexes.os_parsers) {
			os_parsers = regexes.os_parsers.map(function (obj) {
				var regexp = new RegExp(obj.regex),
					osRep  = obj.os_replacement;

				function parser(ua) {
					var os,
						m = ua.match(regexp);

					if (!m) {
						return null;
					}

					os = (osRep ? osRep : m[1]) + (m.length > 2 ? ' ' + m[2] : '') + (m.length > 3 ? '.' + m[3] : '');

					return os;
				}

				return parser;
			});
		}
	};

	/**
	 * @static
	 * @param {string} ua
	 */
	UserAgent.parse = function parse(ua) {
		var os, i, result;

		for (i = 0; i < ua_parsers.length; i += 1) {
			result = ua_parsers[i](ua);
			if (result) {
				break;
			}
		}

		for (i = 0; i < os_parsers.length; i += 1) {
			os = os_parsers[i](ua);
			if (os) {
				break;
			}
		}

		if (!result) {
			result = new UserAgent();
		}

		result.os = os;
		return result;
	};

	UserAgent.prototype.toVersionString = function () {
		var output = '';
		if (this.major !== null) {
			output += this.major;
			if (this.minor !== null) {
				output += '.' + this.minor;
				if (this.patch !== null) {
					output += '.' + this.patch;
				}
			}
		}
		return output;
	};

	UserAgent.prototype.toString = function () {
		var suffix = this.toVersionString();
		if (suffix) {
			suffix = ' ' + suffix;
		}
		return this.family + suffix;
	};

	UserAgent.prototype.toFullString = function () {
		return this.toString() + (this.os ? '/' + this.os : '');
	};

	// Expose for nodejs/browser
	if (typeof module !== 'undefined' && module.exports) {
		module.exports = UserAgent;
	} else {
		global.UserAgent = UserAgent;
	}

}(this));
