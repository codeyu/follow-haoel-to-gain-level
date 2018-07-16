## [73. Set Matrix Zeroes](https://leetcode.com/problems/set-matrix-zeroes/description/)

### 题目描述
Given a m x n matrix, if an element is 0, set its entire row and column to 0. Do it [in-place](https://en.wikipedia.org/wiki/In-place_algorithm).

**Example 1:**
```
Input: 
[
  [1,1,1],
  [1,0,1],
  [1,1,1]
]
Output: 
[
  [1,0,1],
  [0,0,0],
  [1,0,1]
]
```
**Example 2:**
```
Input: 
[
  [0,1,2,0],
  [3,4,5,2],
  [1,3,1,5]
]
Output: 
[
  [0,0,0,0],
  [0,4,5,0],
  [0,3,1,0]
]
```
**Follow up:**

* A straight forward solution using O(mn) space is probably a bad idea.
* A simple improvement uses O(m + n) space, but still not the best solution.
* Could you devise a constant space solution?

### 解题思路
* 考虑把为 0 的行列信息保存起来。
* 要求 O(1)，那只能利用原矩阵做文章。使用第一行和第一列保存为 0 的数据。
* 首先，记录第一行和第一列里是否有为零的数据。
* 然后从第二行和第二列开始遍历，如果有为 0 的数据，保存到第一行和第一列
* 然后遍历行，如果行首为 0，则把这行所有数据置为 0
* 然后遍历列，如果列首为 0，则把这列所有数据置为 0
* 最后判断行首和列首，如果为 0，则把这行和这列的数据都置为 0

### 实现代码

#### go
```go
package main

func setZeroes(matrix [][]int)  {
    r1, c1 := false,false
    r, c := len(matrix), 0
    for _, rows := range matrix {
      c = len(rows)
      break
    }
    for i := 0; i < c; i++{
      if matrix[0][i] == 0{
        r1 = true
        break
      }
    }
    for j := 0; j < r; j++{
      if matrix[j][0] == 0{
        c1 = true
        break
      }
    }
    for i := 1; i < r; i++{
      for j:= 1; j < c; j++{
        if matrix[i][j] == 0{
          matrix[i][0] = 0
          matrix[0][j] = 0
        }
      }
    }
    for i := 1; i < r; i++{
      if matrix[i][0] == 0{
        for j:= 1; j < c; j++{
          matrix[i][j] = 0
        }
      }
      
    }
    for j := 1; j < c; j++{
      if matrix[0][j] == 0{
        for i := 1; i < r; i++{
          matrix[i][j] = 0
        }
      }
    }
    if r1 {
        for j := 0; j < c; j++ {
          matrix[0][j] = 0
      }
    }
    if c1 {
        for i := 0; i < r; i++ {
          matrix[i][0] = 0
      }
    }

}
```