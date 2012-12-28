#!/usr/bin/env python
from setuptools import setup, find_packages
from setuptools.command.install import install as _install


class  install(_install):
    def run(self):
        # After installation, convert regexes.yaml to regexes.json
        _install.run(self)
        import json
        import yaml
        yaml_file = open('regexes.yaml')
        yaml = yaml.load(yaml_file)
        yaml_file.close()
        json_file = open('regexes.json', 'w')
        json_file.write(json.dumps(yaml))
        json_file.close()

setup(
    name='ua_parser',
    version='1.0',
    description='',
    author='PBS',
    author_email='no-reply@pbs.org',
    packages=find_packages('py'),
    package_dir={'': 'py'},
    license='LICENSE.txt',
    zip_safe=False,
    url='https://github.com/tobie/ua-parser',
    include_package_data=True,
    package_data={'': ['README.markdown']},
    data_files=[('', ['regexes.yaml'])],
    install_requires=[
        'pyyaml',
    ],
    #cmdclass={'install': install},
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
