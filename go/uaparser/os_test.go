package uaparser

import (
	"fmt"
	"io/ioutil"
	"launchpad.net/goyaml"
	"testing"
)

func osInitTesting(file string) []map[string]string {
	fmt.Print(file + ": ")
	testFile, _ := ioutil.ReadFile(file)
	testMap := make(map[string][]map[string]string)
	_ = goyaml.Unmarshal(testFile, &testMap)
	return testMap["test_cases"]
}

var osDefaultRegexFile string = "../../regexes.yaml"
var osParser *Parser = nil

func osInitParser(regexFile string) {
	if osParser == nil {
		osParser = New(regexFile)
	}
}

func osTestHelper(file string) bool {
	osInitParser(osDefaultRegexFile)
	tests := osInitTesting(file)
	for _, test := range tests {

		// Other language ports of ua_parser skips js_ua in testing
		if test["js_ua"] != "" {
			continue
		}

		testingString := test["user_agent_string"]
		os := osParser.ParseOs(testingString)

		if os.Family != test["family"] || os.Major != test["major"] ||
			os.Minor != test["minor"] || os.Patch != test["patch"] ||
			os.PatchMinor != test["patch_minor"] {
			fmt.Println("FAIL")
			fmt.Printf("Expected: %v\nActual: %v\n", test, os)
			return false
		}
	}
	return true
}

func TestOs(t *testing.T) {
	if !osTestHelper("../../test_resources/test_user_agent_parser_os.yaml") {
		t.Fail()
	} else {
		fmt.Println("PASS")
	}
}

func TestAdditionalOs(t *testing.T) {
	if !osTestHelper("../../test_resources/additional_os_tests.yaml") {
		t.Fail()
	} else {
		fmt.Println("PASS")
	}
}
