package uaparser_test

import (
	"fmt"
	"io/ioutil"
	"launchpad.net/goyaml"
	"testing"
	. "uaparser"
)

func osInitTesting(file string) []map[string]string {
	fmt.Println("Testing " + file)
	testFile, _ := ioutil.ReadFile(file)
	testMap := make(map[string][]map[string]string)
	_ = goyaml.Unmarshal(testFile, &testMap)
	return testMap["test_cases"]
}

var osDefaultRegexFile string = "regexes.yaml"
var osParser *Parser = nil

func osInitParser(regexFile string) {
	if osParser == nil {
		osParser = New(regexFile)
	}
}

func osHelperTest(file string) bool {
	osInitParser(osDefaultRegexFile)
	tests := osInitTesting(file)
	for _, test := range tests {

		// Other language ports of ua_parser skips js_ua in testing
		if test["js_ua"] != "" {
			continue
		}

		os := new(Os)
		os.Family = ""
		os.Major = ""
		os.Minor = ""
		os.Patch = ""
		os.PatchMinor = ""

		found := false
		testingString := test["user_agent_string"]
		// Attempt to match on all patterns
		for _, osPattern := range osParser.OsPatterns {
			osPattern.Match(testingString, os)
			if len(os.Family) > 0 {
				found = true
				break
			}
		}
		if !found {
			os.Family = "Other"
		}

		if os.Family != test["family"] || os.Major != test["major"] ||
			os.Minor != test["minor"] || os.Patch != test["patch"] ||
			os.PatchMinor != test["patch_minor"] {
			fmt.Errorf(testingString)
			return false
		}
	}
	return true
}

func TestOs(t *testing.T) {
	if !osHelperTest("testing/test_user_agent_parser_os.yaml") {
		t.Fail()
	}
}

func TestAdditionalOs(t *testing.T) {
	if !osHelperTest("testing/additional_os_tests.yaml") {
		t.Fail()
	}
}
