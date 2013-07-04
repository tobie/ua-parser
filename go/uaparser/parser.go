package uaparser

import (
	"bytes"
	"io/ioutil"
	"launchpad.net/goyaml"
	"reflect"
	"regexp"
	"sync"
)

type Parser struct {
	UserAgentPatterns []UserAgentPattern
	OsPatterns        []OsPattern
	DevicePatterns    []DevicePattern
}

func GetExportedName(src string) string {
	byteSrc := []byte(src)
	chunks := exportedNameRegex.FindAll(byteSrc, -1)
	for idx, val := range chunks {
		chunks[idx] = bytes.Title(val)
	}
	return string(bytes.Join(chunks, nil))
}

func ToStruct(interfaceArr []map[string]string, typeInterface interface{}, returnVal *[]interface{}) {
	structArr := make([]interface{}, 0)
	for _, interfaceMap := range interfaceArr {
		structValPtr := reflect.New(reflect.TypeOf(typeInterface))
		structVal := structValPtr.Elem()
		for key, value := range interfaceMap {
			structVal.FieldByName(GetExportedName(key)).SetString(value)
		}
		structArr = append(structArr, structVal.Interface())
	}
	*returnVal = structArr
}

var exportedNameRegex = regexp.MustCompile("[0-9A-Za-z]+")

func New(regexFile string) *Parser {
	parser := new(Parser)

	data, err := ioutil.ReadFile(regexFile)
	if nil != err {
		panic(err)
	}

	m := make(map[string][]map[string]string)
	err = goyaml.Unmarshal(data, &m)
	if err != nil {
		panic(err)
	}

	var wg sync.WaitGroup

	uaPatternType := new(UserAgentPattern)
	var uaInterfaces []interface{}
	var uaPatterns []UserAgentPattern

	wg.Add(1)
	go func() {
		// Read the YAML map and coerce it into a struct
		ToStruct(m["user_agent_parsers"], *uaPatternType, &uaInterfaces)
		uaPatterns = make([]UserAgentPattern, len(uaInterfaces))
		for i, inter := range uaInterfaces {
			uaPatterns[i] = inter.(UserAgentPattern)
			uaPatterns[i].Regexp = regexp.MustCompile(uaPatterns[i].Regex)
		}
		wg.Done()
	}()

	osPatternType := new(OsPattern)
	var osInterfaces []interface{}
	var osPatterns []OsPattern

	wg.Add(1)
	go func() {
		ToStruct(m["os_parsers"], *osPatternType, &osInterfaces)
		osPatterns = make([]OsPattern, len(osInterfaces))
		for i, inter := range osInterfaces {
			osPatterns[i] = inter.(OsPattern)
			osPatterns[i].Regexp = regexp.MustCompile(osPatterns[i].Regex)
		}
		wg.Done()
	}()

	dvcPatternType := new(DevicePattern)
	var dvcInterfaces []interface{}
	var dvcPatterns []DevicePattern

	wg.Add(1)
	go func() {
		ToStruct(m["device_parsers"], *dvcPatternType, &dvcInterfaces)
		dvcPatterns = make([]DevicePattern, len(dvcInterfaces))
		for i, inter := range dvcInterfaces {
			dvcPatterns[i] = inter.(DevicePattern)
			dvcPatterns[i].Regexp = regexp.MustCompile(dvcPatterns[i].Regex)
		}
		wg.Done()
	}()

	wg.Wait()

	parser.UserAgentPatterns = uaPatterns
	parser.OsPatterns = osPatterns
	parser.DevicePatterns = dvcPatterns

	return parser

}
