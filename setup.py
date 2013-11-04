#!/usr/bin/env python
from setuptools import setup
from setuptools.command.develop import develop as _develop
from setuptools.command.sdist   import sdist   as _sdist

def install_regexes():
    print('Copying regexes.yaml to package directory...')
    import os
    import shutil
    cwd = os.path.abspath(os.path.dirname(__file__))
    yaml_src = os.path.join(cwd, 'regexes.yaml')
    if not os.path.exists(yaml_src):
        raise RuntimeError(
                  'Unable to find regexes.yaml, should be at %r' % yaml_src)
    yaml_dest = os.path.join(cwd, 'py', 'ua_parser', 'regexes.yaml')
    shutil.copy2(yaml_src, yaml_dest)

    print('Converting regexes.yaml to regexes.json...')
    import json
    import yaml
    json_dest = yaml_dest.replace('.yaml', '.json')
    regexes = yaml.load(open(yaml_dest))
    json.dump(regexes, open(json_dest, 'w'))

class develop(_develop):
    def run(self):
        install_regexes()
        _develop.run(self)

class sdist(_sdist):
    def run(self):
        install_regexes()
        _sdist.run(self)

setup(
    name='ua-parser',
    version='0.3.4',
    description="Python port of Browserscope's user agent parser",
    author='PBS',
    author_email='no-reply@pbs.org',
    packages=['ua_parser'],
    package_dir={'': 'py'},
    license='LICENSE.txt',
    zip_safe=False,
    url='https://github.com/tobie/ua-parser',
    include_package_data=True,
    package_data={'ua_parser': ['regexes.yaml', 'regexes.json']},
#   install_requires=['pyyaml'],
    cmdclass={
        'develop': develop,
        'sdist':   sdist,
    },
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
