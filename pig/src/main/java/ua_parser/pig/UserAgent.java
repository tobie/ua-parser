package ua_parser.pig;

import java.io.IOException;

import org.apache.pig.EvalFunc;
import org.apache.pig.data.DataType;
import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.apache.pig.impl.logicalLayer.schema.Schema;
import org.apache.pig.impl.logicalLayer.schema.Schema.FieldSchema;

public class UserAgent extends EvalFunc<Tuple> {

    private PigParser parser;

    public UserAgent() throws IOException {
        parser = PigParser.getParser();
    }

    public Tuple exec(Tuple input) throws IOException {
        if (input == null || input.size() == 0) {
            return null;
        }

        try {
            String agentString = (String) input.get(0);
            ua_parser.UserAgent userAgent = parser.parseUserAgent(agentString);
            if (userAgent == null) {
                return null;
            }

            Tuple output = TupleFactory.getInstance().newTuple(4);
            output.set(0, userAgent.family);
            output.set(1, userAgent.major);
            output.set(2, userAgent.minor);
            output.set(3, userAgent.patch);

            return output;
        } catch (Exception e) {
            throw new IOException("Caught exception processing input row ", e);
        }
    }

    @Override
    public Schema outputSchema(Schema input) {
        try {
            Schema tupleSchema = new Schema();

            tupleSchema.add(new FieldSchema("useragentFamily", DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("useragentMajor",  DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("useragentMinor",  DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("useragentPatch",  DataType.CHARARRAY));
            return tupleSchema;
        } catch (Exception e) {
            return null;
        }
    }

}
