package ua_parser.pig;

import java.io.IOException;

import ua_parser.CachingParser;

/**
 * Pig is the canonical use case for processing massive volumes of clickstream
 * data. One of the main patterns in clickstream data is that you will see the
 * same useragent over and over again. This class introduces an LRU cache to
 * reduce the number of times the parsing is actually done.
 *
 * @author Niels Basjes
 *
 */
public class PigParser extends CachingParser {

    private PigParser() throws IOException {
        super();
    }

    private static PigParser instance = null;

    // ------------------------------------------

    public static PigParser getParser() throws IOException {
        if (instance == null)
            instance = new PigParser();
        return instance;
    }

}
