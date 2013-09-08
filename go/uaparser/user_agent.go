package uaparser

import (
	"regexp"
	"strings"
)

type UserAgent struct {
	Family string
	Major  string
	Minor  string
	Patch  string
}

type UserAgentPattern struct {
	Regexp            *regexp.Regexp
	Regex             string
	FamilyReplacement string
	V1Replacement     string
	V2Replacement     string
}

func (uaPattern *UserAgentPattern) Match(line string, ua *UserAgent) {
	bytes := uaPattern.Regexp.FindStringSubmatch(line)
	if len(bytes) > 0 {
		groupCount := uaPattern.Regexp.NumSubexp()

		if len(uaPattern.FamilyReplacement) > 0 {
			if strings.Contains(uaPattern.FamilyReplacement, "$1") && groupCount >= 1 && len(bytes) >= 2 {
				ua.Family = strings.Replace(uaPattern.FamilyReplacement, "$1", bytes[1], 1)
			} else {
				ua.Family = uaPattern.FamilyReplacement
			}
		} else if groupCount >= 1 {
			ua.Family = bytes[1]
		}

		if len(uaPattern.V1Replacement) > 0 {
			ua.Major = uaPattern.V1Replacement
		} else if groupCount >= 2 {
			ua.Major = bytes[2]
		}

		if len(uaPattern.V2Replacement) > 0 {
			ua.Minor = uaPattern.V2Replacement
		} else if groupCount >= 3 {
			ua.Minor = bytes[3]
			if groupCount >= 4 {
				ua.Patch = bytes[4]
			}
		}
	}
}

func (ua *UserAgent) ToString() string {
	var str string
	if ua.Family != "" {
		str += ua.Family
	}
	version := ua.ToVersionString()
	if version != "" {
		str += " " + version
	}
	return str
}

func (ua *UserAgent) ToVersionString() string {
	var version string
	if ua.Major != "" {
		version += ua.Major
	}
	if ua.Minor != "" {
		version += "." + ua.Minor
	}
	if ua.Patch != "" {
		version += "." + ua.Patch
	}
	return version
}
