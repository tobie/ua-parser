package ua_parser.pig;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.FSDataInputStream;
import org.apache.hadoop.fs.FileSystem;
import org.apache.hadoop.fs.Path;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import ua_parser.CachingParser;

import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

/**
 * Pig is the canonical use case for processing massive volumes of clickstream
 * data. One of the main patterns in clickstream data is that you will see the
 * same useragent over and over again. This class introduces an LRU cache to
 * reduce the number of times the parsing is actually done.
 *
 * @author Niels Basjes
 */
public final class PigParser extends CachingParser {

    Logger logger = LoggerFactory.getLogger(PigParser.class);

    private PigParser() throws IOException {
        super();
    }

    private PigParser(InputStream in) throws IOException {
        super(in);
    }

    private static Map<String, PigParser> instances = new HashMap<String, PigParser>();

    // ------------------------------------------

    public static PigParser getParser() throws IOException {
        if (instances.get(null) == null) {
            instances.put(null, new PigParser());
        }
        return instances.get(null);
    }

    public static PigParser getParser(String path) throws IOException {
        if (instances.get(path) == null) {
            Configuration conf = new Configuration();
            FileSystem fs = FileSystem.get(conf);
            Path inFile = new Path(path);
            FSDataInputStream in = fs.open(inFile);

            instances.put(path, new PigParser(in));
        }
        return instances.get(path);
    }
}
