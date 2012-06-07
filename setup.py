#!/usr/bin/env python
from setuptools import setup, find_packages

dependencies = [
    'pyyaml',
]

dependency_links = [
]

setup(
    name='ua_parser',
    version='1.0',
    description='',
    author='PBS',
    author_email='no-reply@pbs.org',
    packages = find_packages('py'),
    package_dir={'':'py'},
    include_package_data=True,
    data_files=[('',['regexes.yaml'])]
)
