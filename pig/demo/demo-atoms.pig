REGISTER ../target/ua-parser-pig-0.1-SNAPSHOT-job.jar

DEFINE DeviceFamily     ua_parser.pig.device.Family;
DEFINE OsFamily         ua_parser.pig.os.Family;
DEFINE OsMajor          ua_parser.pig.os.Major;
DEFINE OsMinor          ua_parser.pig.os.Minor;
DEFINE OsPatch          ua_parser.pig.os.Patch;
DEFINE OsPatchMinor     ua_parser.pig.os.PatchMinor;
DEFINE UserAgentFamily  ua_parser.pig.useragent.Family;
DEFINE UserAgentMajor   ua_parser.pig.useragent.Major;
DEFINE UserAgentMinor   ua_parser.pig.useragent.Minor;
DEFINE UserAgentPatch   ua_parser.pig.useragent.Patch;


UserAgents =
    Load 'useragents.txt' AS (useragent:chararray);

AgentSpecs =
    FOREACH UserAgents
    GENERATE
             DeviceFamily(useragent)        AS DeviceFamily:chararray,

             OsFamily(useragent)            AS OsFamily:chararray,
             OsMajor(useragent)             AS OsMajor:chararray,
             OsMinor(useragent)             AS OsMinor:chararray,
             OsPatch(useragent)             AS OsPatch:chararray,
             OsPatchMinor(useragent)        AS OsPatchMinor:chararray,

             UserAgentFamily(useragent)     AS UserAgentFamily:chararray,
             UserAgentMajor(useragent)      AS UserAgentMajor:chararray,
             UserAgentMinor(useragent)      AS UserAgentMinor:chararray,
             UserAgentPatch(useragent)      AS UserAgentPatch:chararray,

             useragent                      AS Useragent;

DESCRIBE AgentSpecs;
DUMP AgentSpecs;
