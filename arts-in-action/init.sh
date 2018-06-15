#!/bin/bash

YEAR=`date +%Y`
WEEK=`date +%V`

mkdir -p "$YEAR/week-$WEEK"

list="algorithm review tip share"  
for i in $list; do  
    touch "$YEAR/week-$WEEK/$i.md"
done  

echo "everything is ok"