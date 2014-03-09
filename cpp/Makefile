CC=g++
LDFLAGS=-lboost_regex -lboost_system -lyaml-cpp -lglog
CFLAGS=-std=c++0x -Wall -Werror -g -O3

# wildcard object build target
%.o: %.cpp
	$(CC) -c $(CFLAGS) $*.cpp -o $*.o $(LDFLAGS)
	@$(CC) -MM $(CFLAGS) $*.cpp $(LDFLAGS) > $*.d

uaparser_cpp: libuaparser_cpp.a

libuaparser_cpp.a: UaParser.o
	ar rcs $@ $^

UaParserTest: libuaparser_cpp.a UaParserTest.o
	$(CC) $(CFLAGS) $^ -o $@ libuaparser_cpp.a $(LDFLAGS) -lgtest -lpthread

test: UaParserTest
	./UaParserTest

# clean everything generated
clean:
	find . -name "*.o" -exec rm -rf {} \; # clean up object files
	find . -name "*.d" -exec rm -rf {} \; # clean up dependencies
	rm -f UaParserTest *.a

# automatically include the generated *.d dependency make targets
# that are created from the wildcard %.o build target above
-include $(OBJS:.o=.d)
