Usage
========

    package main

    import (
      "../uaparser" // You could change this to a github repo as well
      "fmt"
    )

    func main() {
      testStr := "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-us; Silk/1.1.0-80) AppleWebKit/533.16 (KHTML, like Gecko) Version/5.0 Safari/533.16 Silk-Accelerated=true"
      regexFile := "../../regexes.yaml"
      parser := uaparser.New(regexFile)
      client := parser.Parse(testStr)
      fmt.Println(client.UserAgent.Family)  // "Amazon Silk"
      fmt.Println(client.UserAgent.Major)   // "1"
      fmt.Println(client.UserAgent.Minor)   // "1"
      fmt.Println(client.UserAgent.Patch)   // "0-80"
      fmt.Println(client.Os.Family)         // "Android"
      fmt.Println(client.Os.Major)          // ""
      fmt.Println(client.Os.Minor)          // ""
      fmt.Println(client.Os.Patch)          // ""
      fmt.Println(client.Os.PatchMinor)     // ""
      fmt.Println(client.Device.Family)     // "Kindle Fire"
    }

Testing
========

Includes all the tests in `test_resources`

    go test

Author
========

* Yihuan Zhou

Based on the Java implementation by Steve Jiang and using agent data from BrowserScope