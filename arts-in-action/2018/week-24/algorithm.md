## [189. Rotate Array](https://leetcode.com/problems/rotate-array/)

### 题目描述
Given an array, rotate the array to the right by k steps, where k is non-negative.

**Example 1:**
```
Input: [1,2,3,4,5,6,7] and k = 3
Output: [5,6,7,1,2,3,4]
Explanation:
rotate 1 steps to the right: [7,1,2,3,4,5,6]
rotate 2 steps to the right: [6,7,1,2,3,4,5]
rotate 3 steps to the right: [5,6,7,1,2,3,4]
```
**Example 2:**
```
Input: [-1,-100,3,99] and k = 2
Output: [3,99,-1,-100]
Explanation: 
rotate 1 steps to the right: [99,-1,-100,3]
rotate 2 steps to the right: [3,99,-1,-100]
```
**Note:**

* Try to come up as many solutions as you can, there are at least 3 different ways to solve this problem.
* Could you do it in-place with O(1) extra space?

### 解题思路
1. 当 k 是数组长度的整数倍时，数组的值位置不会变，所以我们要关心的应该是 `k % nums.size` 大于 0 的情况。
2.  观察例子可以发现，我们可以先逆序[0, k]位置的元素，再逆序[k+1, end]位置的元素，最后在整体逆序得到答案。
3. 但同时要考虑各种边角情况如：数组为空等，所以我们取的分界点为 `nums.size-k-1`

### 实现代码

#### go
```go
package main

func rotate(nums []int, k int)  {
    n := len(nums)
    k = k%n
    if len(nums) <=1 || k == 0{
        return
    }
    reverse(nums, 0, n-k-1)
    reverse(nums, n-k, n-1)
    reverse(nums, 0, n-1)
}

func reverse(nums []int, begin int, end int){
    for begin < end {
        var temp = nums[begin];
        nums[begin] = nums[end];
        nums[end] = temp;
        begin++;
        end--;
    }
}
```