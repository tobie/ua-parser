REGISTER ../target/ua-parser-pig-0.1-SNAPSHOT-job.jar

DEFINE Device           ua_parser.pig.Device;
DEFINE Os               ua_parser.pig.Os;
DEFINE UserAgent        ua_parser.pig.UserAgent;


UserAgents =
    Load 'useragents.txt' AS (useragent:chararray);

AgentSpecs =
    FOREACH UserAgents
    GENERATE
             Device(useragent)              AS Device,
             Os(useragent)                  AS Os,
             UserAgent(useragent)           AS UserAgent,

             useragent                      AS Useragent;

DESCRIBE AgentSpecs;
DUMP AgentSpecs;
