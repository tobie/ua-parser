#!/usr/bin/env python
import os
import shutil
import sys

from setuptools import setup, find_packages
from setuptools.command.install import install as _install


class install(_install):
    def run(self):
        # After installation:
        # 1. Move regexes.yaml to directory where ua_parser is installed
        # 2. Convert regexes.yaml to regexes.json
        _install.run(self)
        import json
        import yaml
        import ua_parser
        INSTALLATION_DIR = os.path.dirname(ua_parser.__file__)
        source_path = os.path.join(sys.prefix, 'data', 'regexes.yaml')
        destination_path = os.path.join(INSTALLATION_DIR,
                                        'regexes.yaml')
        shutil.move(source_path, destination_path)

        print 'Converting regexes.yaml to json...'
        yaml_file = open(destination_path)
        yaml = yaml.load(yaml_file)
        yaml_file.close()
        json_file = open(os.path.join(INSTALLATION_DIR, 'regexes.json'), 'w')
        json_file.write(json.dumps(yaml))
        json_file.close()


setup(
    name='ua-parser',
    version='0.3.0',
    description="Python port of Browserscope's user agent parser",
    author='PBS',
    author_email='no-reply@pbs.org',
    packages=find_packages('py'),
    package_dir={'': 'py'},
    license='LICENSE.txt',
    zip_safe=False,
    url='https://github.com/tobie/ua-parser',
    include_package_data=True,
    package_data={'': ['README.markdown']},
    data_files=[('data', ['regexes.yaml'])],
    install_requires=['pyyaml'],
    cmdclass={'install': install, 'develop': install},
    classifiers=[
        'Development Status :: 4 - Beta',
        'Environment :: Web Environment',
        'Intended Audience :: Developers',
        'Operating System :: OS Independent',
        'License :: OSI Approved :: Apache Software License',
        'Programming Language :: Python',
        'Topic :: Internet :: WWW/HTTP',
        'Topic :: Software Development :: Libraries :: Python Modules',
    ],
)
