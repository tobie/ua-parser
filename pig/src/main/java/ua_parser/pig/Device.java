package ua_parser.pig;

import java.io.IOException;

import org.apache.pig.EvalFunc;
import org.apache.pig.data.DataType;
import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.apache.pig.impl.logicalLayer.schema.Schema;
import org.apache.pig.impl.logicalLayer.schema.Schema.FieldSchema;

public class Device extends EvalFunc<Tuple> {

    PigParser parser;

    public Device() throws IOException {
        parser = PigParser.getParser();
    }

    public Tuple exec(Tuple input) throws IOException {
        if (input == null || input.size() == 0)
            return null;
        try {
            String agentString = (String) input.get(0);
            ua_parser.Device device = parser.parseDevice(agentString);
            if (device == null) {
                return null;
            }
            Tuple output = TupleFactory.getInstance().newTuple(1);
            output.set(0, device.family);
            return output;
        } catch (Exception e) {
            throw new IOException("Caught exception processing input row ", e);
        }
    }

    @Override
    public Schema outputSchema(Schema input) {
        try {
            Schema tupleSchema = new Schema();
            tupleSchema.add(new FieldSchema("deviceFamily", DataType.CHARARRAY));
            return tupleSchema;
        } catch (Exception e) {
            return null;
        }
    }

}
