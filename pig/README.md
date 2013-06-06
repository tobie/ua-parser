ua_parser Pig Library
======================

This is the Pig implementation of [ua-parser](https://github.com/tobie/ua-parser).
The implementation uses the shared regex patterns and overrides from regexes.yaml.

Build:
------

    cd ../java ; mvn install    
    mvn package

Usage:
--------
```pig
REGISTER ua-parser-pig-0.1-SNAPSHOT-job.jar
 
DEFINE DeviceFamily     ua_parser.pig.device.Family;
DEFINE OsFamily         ua_parser.pig.os.Family;
DEFINE OsMajor          ua_parser.pig.os.Major;
DEFINE OsMinor          ua_parser.pig.os.Minor;
DEFINE OsPatch          ua_parser.pig.os.Patch;
DEFINE OsPatchMinor     ua_parser.pig.os.PatchMinor;
DEFINE UseragentFamily  ua_parser.pig.useragent.Family;
DEFINE UseragentMajor   ua_parser.pig.useragent.Major;
DEFINE UseragentMinor   ua_parser.pig.useragent.Minor;
DEFINE UseragentPatch   ua_parser.pig.useragent.Patch;
 
UserAgents =
    Load 'useragents.txt' AS (useragent:chararray);

AgentSpecs =
    FOREACH  UserAgents
    GENERATE DeviceFamily(useragent)    AS DeviceFamily:chararray,

             OsFamily(useragent)        AS OsFamily:chararray,
             OsMajor(useragent)         AS OsMajor:chararray,
             OsMinor(useragent)         AS OsMinor:chararray,
             OsPatch(useragent)         AS OsPatch:chararray,
             OsPatchMinor(useragent)    AS OsPatchMinor:chararray,

             UseragentFamily(useragent) AS UseragentFamily:chararray,
             UseragentMajor(useragent)  AS UseragentMajor:chararray,
             UseragentMinor(useragent)  AS UseragentMinor:chararray,
             UseragentPatch(useragent)  AS UseragentPatch:chararray,

             useragent                  AS Useragent;
 
DUMP AgentSpecs;
```

Author:
-------

  * Niels Basjes [@nielsbasjes](https://twitter.com/nielsbasjes)

  This is a trivial interface on top of the Java API created by Steve Jiang [@sjiang](https://twitter.com/sjiang)
