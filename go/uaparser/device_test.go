package uaparser_test

import (
	"fmt"
	"io/ioutil"
	"launchpad.net/goyaml"
	"testing"
	. "uaparser"
)

func dvcInitTesting(file string) []map[string]string {
	fmt.Println("Testing " + file)
	testFile, _ := ioutil.ReadFile(file)
	testMap := make(map[string][]map[string]string)
	_ = goyaml.Unmarshal(testFile, &testMap)
	return testMap["test_cases"]
}

var dvcDefaultRegexFile string = "regexes.yaml"
var dvcParser *Parser = nil

func dvcInitParser(regexFile string) {
	if dvcParser == nil {
		dvcParser = New(regexFile)
	}
}

func dvcHelperTest(file string) bool {
	dvcInitParser(dvcDefaultRegexFile)
	tests := dvcInitTesting(file)
	for _, test := range tests {

		// Other language ports of ua_parser skips js_ua in testing
		if test["js_ua"] != "" {
			continue
		}

		dvc := new(Device)
		dvc.Family = ""

		found := false
		testingString := test["user_agent_string"]
		// Attempt to match on all patterns
		for _, dvcPattern := range dvcParser.DevicePatterns {
			dvcPattern.Match(testingString, dvc)
			if len(dvc.Family) > 0 {
				found = true
				break
			}
		}
		if !found {
			dvc.Family = "Other"
		}

		if dvc.Family != test["family"] {
			fmt.Errorf(testingString)
			return false
		}
	}
	return true
}

func TestDevice(t *testing.T) {
	if !dvcHelperTest("testing/test_device.yaml") {
		t.Fail()
	}
}
