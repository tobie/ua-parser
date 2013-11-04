#!/bin/bash

rm pig_*.log
pig -x local -f demo-atoms.pig  > output-atoms.txt
pig -x local -f demo-tuples.pig > output-tuples.txt

echo =========================
cat output-atoms.txt
echo =========================
cat output-tuples.txt
echo =========================
