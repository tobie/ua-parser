Development Tools
=================

This folder contains development tools which may help to add new regular expressions to the `regexes.yaml` file.

From a file containing user-agent strings sorted csv-tables for the different parsers can be generated.
With the csv-tables the parsing results for the given user-agent strings can be compared.

To detect the matching parsing line in the `regexes.yaml`, debug information can be added to the file.

General Files
-------------

**useragents.txt**  
List of user-agents which are used as input. Each line shall contain one user-agent string.

**debuginfo.js**  
Add or remove debug information to the `regexes.yaml` file. Run the file with `node debuginfo.js`.<br>
Please *remove* the debug infomation before commiting the changed `regexes.yaml` file.

Files to generate lists and add test cases
------------------------------------------

**ua.js**  
Parse the user-agents with the ua-parser.

**os.js**  
Parse the user-agents with the os-parser.

**device.js**  
Parse the user-agents with the device-parser.

All files can be used with the following arguments:

* file:[filename] : Instead of "useragents.txt" the file with "filename" is used as input.
* swapdebug:true  : Change the column for showing the regex matcher number from column one. The sorting of the resulting cvs-table will be different. This option allows to check different matchers for same model, brand or family.
* testcases:true  : Generate testcases file. All user-agents encountered in the testcases file will be appended
* appenduas:false : Do not append the user-agents from the testcases file. Use this option with care as you will not be able to check for broken tests.

Development Process
===================

As an example the development process to add and change regular expressions is depicted with adding new devices to the "device_parsers". For any other parser you can follow the same steps with replacing "device.js" by either "os.js" or "ua.js" .

1. Add the debug information to the `regexes.yaml` file. For each "regex" a debug info in the form "#0001" will be added. For each "regex" the number is counted up.
    
    ````
    node devel/debuginfo.js
    ````
    
2. Add your user-agents to the file `useragents.txt`. 
3. Parse the user-agents with the parser you like to change. E.g. here "device_parsers"

    ````
    node devel/device.js
    ````
  
4. Open the csv-output file in a spreadsheet or with

    ````
    less -Six12 device.csv
    ````

5. Check the csv-table if the user-agents were parsed the way they should. In the first column the debug number will be displayed. If this is missing either no match was found (default should be "Other") or the debug information is missing in the `regexes.yaml`. 
6. Change one or more "regex" expressions in the `regexes.yaml` file. Parse the user-agents as in Step 3.
7. Recheck list again. To get a different view by changing the sorting order with family or brand model first use:
    
    ````
    node devel/device.js swapdebug:true
    ````
    
8. If everything is as expected then re-run parsing with involving the testcases
    
    ````
    node devel/device.js testcases:true
    ````
    
9. The run writes the file `test_device.yaml` and `device.log`. In `device.log` all broken tests are reported. The file `test_device.yaml` writes a new testcases file which contains the good results for the changed `regexes.yaml` file. You can also check the differences with:
    
    ````
    diff devel/test_device.yaml test_resources/test_device.yaml
    ````
    
    If you are really sure that your changes do not corrupt the previous testcases, copy the the generated `test_device.yaml` file.

10. Run the mocha tests with:

    ````
    npm test
    ````

11. If these tests did run without any problems then remove the debuginfo from the `regexes.yaml`file.

    ````
    node devel/debuginfo.js
    ````
    
    and commit your changes.

 





