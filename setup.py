#!/usr/bin/env python
import os
import shutil

from pkg_resources import resource_filename
from setuptools import setup, find_packages
from setuptools.command.install import install as _install


class install(_install):
    def run(self):
        # copy regexes.yaml down into the package directory
        print 'Copying regexes.yaml to package directory...'
        import os
        import shutil
        cwd = os.path.abspath('.')
        yaml_src = os.path.join(cwd, 'regexes.yaml')
        yaml_dest = os.path.join(cwd, 'py', 'ua_parser', 'regexes.yaml')
        shutil.copy(yaml_src, yaml_dest)

        # convert yaml to json
        print 'Converting regexes.yaml to json...'
        import json
        import yaml
        regexes = yaml.load(open(yaml_dest))
        json_dest = yaml_dest.replace('.yaml', '.json')
        json.dump(regexes, open(json_dest, 'w'))
        _install.run(self)


setup(
    name='ua-parser',
    version='0.3.3',
    description="Python port of Browserscope's user agent parser",
    author='PBS',
    author_email='no-reply@pbs.org',
    packages=find_packages('py'),
    package_dir={'': 'py'},
    license='LICENSE.txt',
    zip_safe=False,
    url='https://github.com/tobie/ua-parser',
    include_package_data=True,
    package_data={'': ['regexes.yaml', 'regexes.json']},
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
