#!/bin/bash

YEAR=`date +%Y`
WEEK=`date +%V`

if [ "$1" ];then
WEEK=`date -jf "%Y%m%d" "$1" +"%V"`
fi
cd arts-in-action
mkdir -p "$YEAR/week-$WEEK"

list="algorithm review tip share"  
for i in $list; do  
    touch "$YEAR/week-$WEEK/$i.md"
done  
cd ..
echo "everything is ok"