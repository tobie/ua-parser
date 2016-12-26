package ua_parser.pig;

import java.io.IOException;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.pig.EvalFunc;
import org.apache.pig.PigException;
import org.apache.pig.backend.executionengine.ExecException;
import org.apache.pig.data.BagFactory;
import org.apache.pig.data.DataType;
import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.apache.pig.impl.logicalLayer.schema.Schema;
import org.apache.pig.impl.logicalLayer.schema.Schema.FieldSchema;

import ua_parser.CachingParser;
import ua_parser.Client;

/**
 * This UDF accepts a UserAgent string and parses it to return a tuple containing:
 * 
 * (DeviceFamily:chararray, OsFamily:chararray, OsMajor:chararray, OsMinor:chararray, 
 *  OsPatch:chararray, OsPatchMinor:chararray, UseragentFamily:chararray, 
 *  UseragentMajor:chararray, UseragentMinor:chararray, UseragentPatch:chararray)
 * 
 * @author Rohit Rangnekar
 *
 */
public class PigUserAgentParser extends EvalFunc<Tuple> {
    
    private final static Log LOG = LogFactory.getLog(PigUserAgentParser.class);
    
    protected final static TupleFactory tupleFactory = TupleFactory.getInstance();
    protected final static BagFactory bagFactory = BagFactory.getInstance();
    
    private CachingParser parser;
    
    //Tuple with 10 null fields as the output
    private static Tuple nullValue = TupleFactory.getInstance().newTuple(10);
    
    public PigUserAgentParser() {
        this("/ua_parser/regexes.yaml");
    }
    
    public PigUserAgentParser(String localfile) {
        parser = new CachingParser(PigUserAgentParser.class.getResourceAsStream(localfile));
    }
    
    @Override
    public Schema outputSchema(Schema input) {
        Schema tupleSchema = new Schema();
        
        tupleSchema.add(new FieldSchema("DeviceFamily", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("OsFamily", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("OsMajor", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("OsMinor", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("OsPatch", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("OsPatchMinor", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("UseragentFamily", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("UseragentMajor", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("UseragentMinor", DataType.CHARARRAY));
        tupleSchema.add(new FieldSchema("UseragentPatch", DataType.CHARARRAY));
        
        Schema s = new Schema(new FieldSchema(null, tupleSchema));
        return s;
    }
    
    @Override
    public Tuple exec(Tuple input) throws IOException {
        try {
            
            if (input == null || input.size() != 1) return nullValue;
            
            String rawUa = (String) input.get(0);
            if (rawUa == null) return nullValue;
            
            Client info = parser.parse(rawUa);
            
            Tuple t = tupleFactory.newTuple(10);
            
            if (info.device != null) t.set(0, info.device.family);
            
            if (info.os != null) {
                t.set(1, info.os.family);
                t.set(2, info.os.major);
                t.set(3, info.os.minor);
                t.set(4, info.os.patch);
                t.set(5, info.os.patchMinor);
            }
            
            if (info.userAgent != null) {
                t.set(6, info.userAgent.family);
                t.set(7, info.userAgent.major);
                t.set(8, info.userAgent.minor);
                t.set(9, info.userAgent.patch);
            }
            
            return t;
            
        } catch (ExecException ee) {
            throw ee;
        } catch (Exception e) {
            int errCode = 2110;
            String msg = "Error while processing User Agent String in " + this.getClass().getSimpleName();
            throw new ExecException(msg, errCode, PigException.USER_ENVIRONMENT, e);           
        }
    }

}