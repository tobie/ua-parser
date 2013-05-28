Contributing Changes to regexes.yaml
------------------------------------

Contributing to the project, especially `regexes.yaml`, is both welcomed and encouraged. To do so just do the following:

1. Fork the project
2. Create a branch for your changes
3. Modify `regexes.yaml` as appropriate
4. Add tests to the following files and follow their format:
    * `test_resources/test_device.yaml`
    * `test_resources/test_user_agent_parser.yaml`
    * `test_resources/test_user_agent_parser_os.yaml`
5. Push your branch to GitHub and submit a pull request
6. Monitor the pull request to make sure the Travis build succeeds. If it fails simply make the necessary changes to your branch and push it. Travis will re-test the changes.

That's it. If you don't feel comfortable forking the project or modifying the YAML you can also [submit an issue](https://github.com/tobie/ua-parser/issues) that includes the appropriate user agent string and the expected results of parsing.

Thanks!