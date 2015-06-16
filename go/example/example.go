package main

import (
	"fmt"
	"io/ioutil"
	"os" // You could change this to a github repo as well

	"../uaparser"
)

func main() {
	testBytes, err := ioutil.ReadAll(os.Stdin)

	if err != nil {
		panic(err)
	}

	testStr := string(testBytes)
	regexFile := "../../regexes.yaml"
	parser := uaparser.New(regexFile)
	client := parser.Parse(testStr)
	fmt.Println(testStr)
	fmt.Println("UserAgent: " + client.UserAgent.ToString())
	fmt.Println("OS: " + client.Os.ToString())
	fmt.Println("Device: " + client.Device.ToString())
}
