package ua_parser.pig;

import java.io.IOException;

import junit.framework.TestCase;

import org.apache.pig.data.Tuple;
import org.apache.pig.data.TupleFactory;
import org.junit.Test;

public class PigTest extends TestCase {
    @Test
    public void testPigFuncsSeparateFields() throws IOException {
        Tuple tuple = TupleFactory.getInstance().newTuple(1);
        tuple.set(0, "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_3 like Mac OS X) "
                + "AppleWebKit/534.46 (KHTML, like Gecko) "
                + "Version/5.1 Mobile/9B179 Safari/7534.48.3");

        assertEquals("iPhone",          (new ua_parser.pig.device.Family()).exec(tuple));
        assertEquals("Apple",           (new ua_parser.pig.device.Brand()).exec(tuple));
        assertEquals("iPhone",          (new ua_parser.pig.device.Model()).exec(tuple));

        assertEquals("iOS",             (new ua_parser.pig.os.Family()).exec(tuple));
        assertEquals("5",               (new ua_parser.pig.os.Major()).exec(tuple));
        assertEquals("1",               (new ua_parser.pig.os.Minor()).exec(tuple));
        assertEquals("3",               (new ua_parser.pig.os.Patch()).exec(tuple));
        assertEquals(null,              (new ua_parser.pig.os.PatchMinor()).exec(tuple));

        assertEquals("Mobile Safari",   (new ua_parser.pig.useragent.Family()).exec(tuple));
        assertEquals("5",               (new ua_parser.pig.useragent.Major()).exec(tuple));
        assertEquals("1",               (new ua_parser.pig.useragent.Minor()).exec(tuple));
        assertEquals(null,              (new ua_parser.pig.useragent.Patch()).exec(tuple));

        // Run the same set twice
        assertEquals("iPhone",          (new ua_parser.pig.device.Family()).exec(tuple));
        assertEquals("Apple",           (new ua_parser.pig.device.Brand()).exec(tuple));
        assertEquals("iPhone",          (new ua_parser.pig.device.Model()).exec(tuple));

        assertEquals("iOS",             (new ua_parser.pig.os.Family()).exec(tuple));
        assertEquals("5",               (new ua_parser.pig.os.Major()).exec(tuple));
        assertEquals("1",               (new ua_parser.pig.os.Minor()).exec(tuple));
        assertEquals("3",               (new ua_parser.pig.os.Patch()).exec(tuple));
        assertEquals(null,              (new ua_parser.pig.os.PatchMinor()).exec(tuple));

        assertEquals("Mobile Safari",   (new ua_parser.pig.useragent.Family()).exec(tuple));
        assertEquals("5",               (new ua_parser.pig.useragent.Major()).exec(tuple));
        assertEquals("1",               (new ua_parser.pig.useragent.Minor()).exec(tuple));
        assertEquals(null,              (new ua_parser.pig.useragent.Patch()).exec(tuple));
    }

    @Test
    public void testPigFuncsPerTypeCombined() throws IOException {
        Tuple tuple = TupleFactory.getInstance().newTuple(1);
        tuple.set(0, "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_3 like Mac OS X) "
                + "AppleWebKit/534.46 (KHTML, like Gecko) "
                + "Version/5.1 Mobile/9B179 Safari/7534.48.3");

        assertEquals("(iPhone,Apple,iPhone)",  (new ua_parser.pig.Device()).exec(tuple).toString());
        assertEquals("(iOS,5,1,3,)",           (new ua_parser.pig.Os()).exec(tuple).toString());
        assertEquals("(Mobile Safari,5,1,)",   (new ua_parser.pig.UserAgent()).exec(tuple).toString());

        // Run the same set twice
        assertEquals("(iPhone,Apple,iPhone)",  (new ua_parser.pig.Device()).exec(tuple).toString());
        assertEquals("(iOS,5,1,3,)",           (new ua_parser.pig.Os()).exec(tuple).toString());
        assertEquals("(Mobile Safari,5,1,)",   (new ua_parser.pig.UserAgent()).exec(tuple).toString());
    }

    
    @Test
    public void testNullInput() throws IOException {

        assertEquals(null, (new ua_parser.pig.device.Family()).exec(null));

        assertEquals(null, (new ua_parser.pig.os.Family()).exec(null));
        assertEquals(null, (new ua_parser.pig.os.Major()).exec(null));
        assertEquals(null, (new ua_parser.pig.os.Minor()).exec(null));
        assertEquals(null, (new ua_parser.pig.os.Patch()).exec(null));
        assertEquals(null, (new ua_parser.pig.os.PatchMinor()).exec(null));

        assertEquals(null, (new ua_parser.pig.useragent.Family()).exec(null));
        assertEquals(null, (new ua_parser.pig.useragent.Major()).exec(null));
        assertEquals(null, (new ua_parser.pig.useragent.Minor()).exec(null));
        assertEquals(null, (new ua_parser.pig.useragent.Patch()).exec(null));

    }

}
