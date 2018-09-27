## [28. Implement strStr()](https://leetcode.com/problems/implement-strstr/description/)

### 题目描述
Implement [strStr()](http://www.cplusplus.com/reference/cstring/strstr/).

Return the index of the first occurrence of needle in haystack, or **-1** if needle is not part of haystack.

**Example 1:**
```
Input: haystack = "hello", needle = "ll"
Output: 2
```
**Example 2:**
```
Input: haystack = "aaaaa", needle = "bba"
Output: -1
```
**Clarification:**

What should we return when `needle` is an empty string? This is a great question to ask during an interview.

For the purpose of this problem, we will return 0 when `needle` is an empty string. This is consistent to C's [strstr()](http://www.cplusplus.com/reference/cstring/strstr/) and Java's [indexOf()](https://docs.oracle.com/javase/7/docs/api/java/lang/String.html#indexOf(java.lang.String)).
### 解题思路
* 这是个字符串匹配问题。自然想到KMP算法求解。
* KMP算法的关键是计算next数组。
* 另外，字符串匹配算法经常使用的还有 Boyer-Moore（BM） 算法，sunday算法。
* 

### 实现代码

#### go
```go
package main
func strStr(haystack string, needle string) int {
    h:=len(haystack)
	n:=len(needle)
    if n==0	{
        return 0
    }
	if h==0||h<n {
        return -1
    }
	
    next := make([]int, n)
	getNext(needle,next)
	i,j:=0,0
	for i<h&&j<n {
		if j==-1||haystack[i]==needle[j]{
			i++
			j++
		}else{
			j=next[j]
        }
	}
	if j==n{
        return i-j
    }	
	return -1
}
func getNext(str string, next []int){
    len := len(str)
    next[0] = -1;
    j, k := 0, -1
    for j<len-1{
        if k==-1 || str[j]==str[k]{
            j++
            k++
            next[j] = k
        }else{
            k = next[k]
        }
    
    }
}

```