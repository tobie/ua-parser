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
      fmt.Println(testStr)
      fmt.Println("UserAgent")
      fmt.Println("Family: " + client.UserAgent.Family)
      fmt.Println("Major: " + client.UserAgent.Major)
      fmt.Println("Minor: " + client.UserAgent.Minor)
      fmt.Println("Patch: " + client.UserAgent.Patch)
      fmt.Println("OS")
      fmt.Println("Family: " + client.Os.Family)
      fmt.Println("Major: " + client.Os.Major)
      fmt.Println("Minor: " + client.Os.Minor)
      fmt.Println("Patch: " + client.Os.Patch)
      fmt.Println("PatchMinor: " + client.Os.PatchMinor)
      fmt.Println("Device")
      fmt.Println("Family: " + client.Device.Family)
    }

Author
=========

* Yihuan Zhou

Based on the Java implementation by Steve Jiang and using agent data from BrowserScope