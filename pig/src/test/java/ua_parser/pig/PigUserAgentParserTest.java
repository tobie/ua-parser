package ua_parser.pig;

import java.io.IOException;

import junit.framework.TestCase;

import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.junit.Test;

public class PigUserAgentParserTest extends TestCase {
    
    @Test
    public void testPigFuncs() throws IOException {
        
        PigUserAgentParser parser = new PigUserAgentParser();
        
        Tuple tuple1 = TupleFactory.getInstance().newTuple(1);
        tuple1.set(0, "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_3 like Mac OS X) "
                + "AppleWebKit/534.46 (KHTML, like Gecko) "
                + "Version/5.1 Mobile/9B179 Safari/7534.48.3");
        Tuple output1 = parser.exec(tuple1);

        assertEquals("iPhone",          output1.get(0));

        assertEquals("iOS",             output1.get(1));
        assertEquals("5",               output1.get(2));
        assertEquals("1",               output1.get(3));
        assertEquals("3",               output1.get(4));
        assertEquals(null,              output1.get(5));

        assertEquals("Mobile Safari",   output1.get(6));
        assertEquals("5",               output1.get(7));
        assertEquals("1",               output1.get(8));
        assertEquals(null,              output1.get(9));

        // Run a new test with the same parser
        
        Tuple tuple2 = TupleFactory.getInstance().newTuple(1);
        tuple2.set(0, "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.4; fr; rv:1.9.1.5) "
                    + "Gecko/20091102 Firefox/3.5.5,gzip(gfe),gzip(gfe)");
        Tuple output2 = parser.exec(tuple2);

        assertEquals("Other",           output2.get(0));

        assertEquals("Mac OS X",        output2.get(1));
        assertEquals("10",              output2.get(2));
        assertEquals("4",               output2.get(3));
        assertEquals(null,              output2.get(4));
        assertEquals(null,              output2.get(5));

        assertEquals("Firefox",         output2.get(6));
        assertEquals("3",               output2.get(7));
        assertEquals("5",               output2.get(8));
        assertEquals("5",               output2.get(9));
    }

    @Test
    public void testEmptyInput() throws IOException {
        
        PigUserAgentParser parser = new PigUserAgentParser();

        Tuple tuple1 = TupleFactory.getInstance().newTuple(1);
        tuple1.set(0, "");
        Tuple output1 = parser.exec(tuple1);

        assertEquals("Other",           output1.get(0));

        assertEquals("Other",           output1.get(1));
        assertEquals(null,              output1.get(2));
        assertEquals(null,              output1.get(3));
        assertEquals(null,              output1.get(4));
        assertEquals(null,              output1.get(5));

        assertEquals("Other",           output1.get(6));
        assertEquals(null,              output1.get(7));
        assertEquals(null,              output1.get(8));
        assertEquals(null,              output1.get(9));

    }
    
    @Test
    public void testNullInput() throws IOException {
        
        PigUserAgentParser parser = new PigUserAgentParser();

        Tuple tuple1 = TupleFactory.getInstance().newTuple(1);
        tuple1.set(0, null);
        Tuple output1 = parser.exec(tuple1);

        assertEquals(null,              output1.get(0));

        assertEquals(null,              output1.get(1));
        assertEquals(null,              output1.get(2));
        assertEquals(null,              output1.get(3));
        assertEquals(null,              output1.get(4));
        assertEquals(null,              output1.get(5));

        assertEquals(null,              output1.get(6));
        assertEquals(null,              output1.get(7));
        assertEquals(null,              output1.get(8));
        assertEquals(null,              output1.get(9));

    }

}
