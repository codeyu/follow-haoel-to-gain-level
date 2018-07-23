## [7. Reverse Integer](https://leetcode.com/problems/reverse-integer/description/)

### 题目描述
Given a 32-bit signed integer, reverse digits of an integer.

**Example 1:**
```
Input: 123
Output: 321
```
**Example 2:**
```
Input: -123
Output: -321
```
**Example 3:**
```
Input: 120
Output: 21
```
**Note:**

Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−2^31,  2^31 − 1]. For the purpose of this problem, assume that your function returns 0 when the reversed integer overflows.

### 解题思路

除十取余法

### 实现代码

#### go
```go
package main
import "math"
func reverse(x int) int {
    if x > math.MaxInt32 || x < math.MinInt32 || x == 0{
        return 0
    }
    isNag := false
    if x < 0{
        isNag = true   
    }
    x = int(math.Abs(float64(x)))
    lastDigit, reverseNum := 0,0
    for{
        if x != 0{
            lastDigit = x % 10
            reverseNum = reverseNum * 10 + lastDigit
            x = x/10
        }else{
            break
        }
    }
    if isNag {
        reverseNum =  reverseNum * -1
    }
    if reverseNum > math.MaxInt32 || reverseNum < math.MinInt32{
        return 0
    }
    return reverseNum
}
```