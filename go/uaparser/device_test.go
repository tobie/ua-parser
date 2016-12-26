package uaparser

import (
	"fmt"
	"gopkg.in/yaml.v2"
	"io/ioutil"
	"testing"
)

func dvcInitTesting(file string) []map[string]string {
	fmt.Print(file + ": ")
	testFile, _ := ioutil.ReadFile(file)
	testMap := make(map[string][]map[string]string)
	_ = yaml.Unmarshal(testFile, &testMap)
	return testMap["test_cases"]
}

var dvcDefaultRegexFile string = "../../regexes.yaml"
var dvcParser *Parser = nil

func dvcInitParser(regexFile string) {
	if dvcParser == nil {
		dvcParser, _ = New(regexFile)
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

		testingString := test["user_agent_string"]
		dvc := dvcParser.ParseDevice(testingString)

		if dvc.Family != test["family"] {
			fmt.Println("FAIL")
			fmt.Printf("Expected: %v\nActual: %v\n", test, dvc)
			return false
		}
	}
	return true
}

func TestDevice(t *testing.T) {
	if !dvcHelperTest("../../test_resources/test_device.yaml") {
		t.Fail()
	} else {
		fmt.Println("PASS")
	}
}
