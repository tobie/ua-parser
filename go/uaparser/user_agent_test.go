package uaparser_test

import (
	"fmt"
	"io/ioutil"
	"launchpad.net/goyaml"
	"testing"
	. "uaparser"
)

func uaInitTesting(file string) []map[string]string {
	fmt.Println("Testing " + file)
	testFile, _ := ioutil.ReadFile(file)
	testMap := make(map[string][]map[string]string)
	_ = goyaml.Unmarshal(testFile, &testMap)
	return testMap["test_cases"]
}

var uaDefaultRegexFile string = "regexes.yaml"
var uaParser *Parser = nil

func uaInitParser(regexFile string) {
	if uaParser == nil {
		uaParser = New(regexFile)
	}
}

func uaHelperTest(file string) bool {
	uaInitParser(uaDefaultRegexFile)
	tests := uaInitTesting(file)
	for _, test := range tests {

		// Other language ports of ua_parser skips js_ua in testing
		if test["js_ua"] != "" {
			continue
		}

		ua := new(UserAgent)
		ua.Family = ""
		ua.Major = ""
		ua.Minor = ""
		ua.Patch = ""

		found := false
		testingString := test["user_agent_string"]
		// Attempt to match on all patterns
		for _, uaPattern := range uaParser.UserAgentPatterns {
			uaPattern.Match(testingString, ua)
			if len(ua.Family) > 0 {
				found = true
				break
			}
		}
		if !found {
			ua.Family = "Other"
		}

		if ua.Family != test["family"] || ua.Major != test["major"] ||
			ua.Minor != test["minor"] || ua.Patch != test["patch"] {
			fmt.Errorf(testingString)
			return false
		}
	}
	return true
}

func TestUserAgent(t *testing.T) {
	if !uaHelperTest("testing/test_user_agent_parser.yaml") {
		t.Fail()
	}
}

func TestFirefoxUserAgents(t *testing.T) {
	if !uaHelperTest("testing/firefox_user_agent_strings.yaml") {
		t.Fail()
	}
}

func TestPgtsBrowsersList(t *testing.T) {
	if !uaHelperTest("testing/pgts_browsers_list.yaml") {
		t.Fail()
	}
}
