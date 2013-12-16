package ua_parser.pig.device;

import java.io.IOException;

import org.apache.pig.EvalFunc;
import org.apache.pig.data.Tuple;

import ua_parser.Device;
import ua_parser.pig.PigParser;

public class Brand extends EvalFunc<String> {

    private PigParser parser;

    public Brand() throws IOException {
        parser = PigParser.getParser();
    }

    public String exec(Tuple input) throws IOException {
        if (input == null || input.size() == 0) {
            return null;
        }

        try {
            String agentString = (String) input.get(0);
            Device device = parser.parseDevice(agentString);
            if (device == null) {
                return null;
            }
            return device.brand;
        } catch (Exception e) {
            throw new IOException("Caught exception processing input row ", e);
        }
    }

}
