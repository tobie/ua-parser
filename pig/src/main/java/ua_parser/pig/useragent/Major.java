package ua_parser.pig.useragent;

import java.io.IOException;

import org.apache.pig.EvalFunc;
import org.apache.pig.data.Tuple;

import ua_parser.UserAgent;
import ua_parser.pig.PigParser;

public class Major extends EvalFunc<String> {

    private PigParser parser;

    public Major() throws IOException {
        parser = PigParser.getParser();
    }

    public String exec(Tuple input) throws IOException {
        if (input == null || input.size() == 0) {
            return null;
        }

        try {
            String agentString = (String) input.get(0);
            UserAgent userAgent = parser.parseUserAgent(agentString);
            if (userAgent == null) {
                return null;
            }
            return userAgent.major;
        } catch (Exception e) {
            throw new IOException("Caught exception processing input row ", e);
        }
    }

}
