#!/bin/bash

rm pig_*.log
pig -x local -f demo.pig > output.txt

echo =========================
cat output.txt
echo =========================
