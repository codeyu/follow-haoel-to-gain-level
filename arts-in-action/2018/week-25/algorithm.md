## [217. Contains Duplicate](https://leetcode.com/problems/contains-duplicate/)

### 题目描述
Given an array of integers, find if the array contains any duplicates.

Your function should return true if any value appears at least twice in the array, and it should return false if every element is distinct.

**Example 1:**
```
Input: [1,2,3,1]
Output: true
```
**Example 2:**
```
Input: [1,2,3,4]
Output: false
```
**Example 3:**
```
Input: [1,1,1,3,3,4,3,2,4,2]
Output: true
```
### 解题思路
1. 查找数组中重复的项，可以借助集合数据类型，遍历数组，插入集合，若插入失败，则说明有重复项。
2. 第二种解法是先把数组排序，然后遍历数组，比较前后项是否相等。

### 实现代码

#### go
```go
package main

func containsDuplicate(nums []int) bool {
    aMap := make(map[int]int)
    for index, item := range nums{
        if _, found := aMap[item]; found{
            return true
        }else{
            aMap[item] = index
        }
    }
    return false  
} 
```