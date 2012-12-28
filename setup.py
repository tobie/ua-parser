#!/usr/bin/env python
from setuptools import setup, find_packages

setup(
    name='ua-parser',
    version='0.3.0',
    description="Python port of Browserscope's user agent parser",
    author='PBS',
    author_email='no-reply@pbs.org',
    packages=find_packages('py'),
    package_dir={'':'py'},
    license='LICENSE.txt',
    zip_safe=False,
    url='https://github.com/tobie/ua-parser',
    include_package_data=True,
    package_data = { '': ['README.markdown'] },
    data_files=[('',['regexes.yaml'])],
    install_requires = [
        'pyyaml',
    ],
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
