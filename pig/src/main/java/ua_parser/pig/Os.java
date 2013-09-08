package ua_parser.pig;

import java.io.IOException;

import org.apache.pig.EvalFunc;
import org.apache.pig.data.DataType;
import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.apache.pig.impl.logicalLayer.schema.Schema;
import org.apache.pig.impl.logicalLayer.schema.Schema.FieldSchema;

import ua_parser.OS;

public class Os extends EvalFunc<Tuple> {

    private PigParser parser;

    public Os() throws IOException {
        parser = PigParser.getParser();
    }

    public Tuple exec(Tuple input) throws IOException {
        if (input == null || input.size() == 0) {
            return null;
        }

        try {
            String agentString = (String) input.get(0);
            OS os = parser.parseOS(agentString);
            if (os == null) {
                return null;
            }

            Tuple output = TupleFactory.getInstance().newTuple(5);
            output.set(0, os.family);
            output.set(1, os.major);
            output.set(2, os.minor);
            output.set(3, os.patch);
            output.set(4, os.patchMinor);

            return output;
        } catch (Exception e) {
            throw new IOException("Caught exception processing input row ", e);
        }
    }

    @Override
    public Schema outputSchema(Schema input) {
        try {
            Schema tupleSchema = new Schema();

            tupleSchema.add(new FieldSchema("osFamily",     DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("osMajor",      DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("osMinor",      DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("osPatch",      DataType.CHARARRAY));
            tupleSchema.add(new FieldSchema("osPatchMinor", DataType.CHARARRAY));
            return tupleSchema;
        } catch (Exception e) {
            return null;
        }
    }

}
