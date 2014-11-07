PWD = $(shell pwd)

all: prep test 

prep: prep-py prep-php

test: test-js test-py test-php test-perl test-java

clean: clean-py clean-php clean-perl

# ----
test-js:
	npm test

# ----
prep-py:
	sudo apt-get install python-yaml
	#python setup.py develop

test-py:
	cp regexes.yaml py/ua_parser/regexes.yaml
	python py/ua_parser/user_agent_parser_test.py

clean-py:
	rm py/ua_parser/user_agent_parser.pyc py/ua_parser/regexes.yaml 

# ----
prep-php:
	@sudo apt-get install php5-cli
	@wget https://getcomposer.org/composer.phar && php composer.phar install

test-php:
	php php/bin/uaparser.php ua-parser:convert
	cd php && ../vendor/bin/phpunit --exclude-group performance
	@cd $(PWD)

clean-php:
	rm -rf vendor
	#rm composer.lock composer.phar
	
# ----
test-perl:
	cd perl && perl Makefile.PL
	cd perl && make
	cd perl && export DEV_TESTS=1 && make test
	@cd $(PWD)

clean-perl:
	cd perl && make clean
	@cd $(PWD)

# ----
test-java:
	cd java && mvn -Dtest=UserAgentTest,OSTest,DeviceTest,ParserTest test
	@cd $(PWD)

# ----
.PHONY: all clean
