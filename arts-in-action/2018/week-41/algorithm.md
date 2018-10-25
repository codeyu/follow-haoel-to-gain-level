## [51. N-Queens](https://leetcode.com/problems/n-queens/)
### 题目描述
The n-queens puzzle is the problem of placing n queens on an n×n chessboard such that no two queens attack each other.

![](https://assets.leetcode.com/uploads/2018/10/12/8-queens.png)

Given an integer n, return all distinct solutions to the n-queens puzzle.

Each solution contains a distinct board configuration of the n-queens' placement, where `'Q'` and `'.'` both indicate a queen and an empty space respectively.

**Example:**
```
Input: 4
Output: [
 [".Q..",  // Solution 1
  "...Q",
  "Q...",
  "..Q."],

 ["..Q.",  // Solution 2
  "Q...",
  "...Q",
  ".Q.."]
]
```
**Explanation:** There exist two distinct solutions to the 4-queens puzzle as shown above.
### 解题思路
`皇后（Queen）`是最强的棋子，可横走、直走或斜走，移动步数不限，但不可转向或越过其他棋子。
如下图所示：图中“X”表示可走的位置。
[补图](https://zh.wikibooks.org/zh-hans/%E5%9B%BD%E9%99%85%E8%B1%A1%E6%A3%8B/%E8%A7%84%E5%88%99)
所以，要是每个皇后都不能攻击对方，最直观的感受就是：每行，每列有且只能有1个皇后。

### 实现代码

#### go
```go
package main
func solveNQueens(n int) [][]string {
    
}

```