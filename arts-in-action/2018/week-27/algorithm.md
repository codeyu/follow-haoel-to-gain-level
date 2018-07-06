## [206. Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/description/)

### 题目描述
Reverse a singly linked list.

**Example:**
```
Input: 1->2->3->4->5->NULL
Output: 5->4->3->2->1->NULL
```
**Follow up:**

A linked list can be reversed either iteratively or recursively. Could you implement both?

### 解题思路


### 实现代码

#### go
```go
package main
/**
 * Definition for singly-linked list.
 * type ListNode struct {
 *     Val int
 *     Next *ListNode
 * }
 */

type ListNode struct {
    Val int
    Next *ListNode
}

func reverseList(head *ListNode) *ListNode {
    if head == nil || head.Next == nil{
        return head
    }
    var ret *ListNode = nil
    cur := head
    for{
        if(cur == nil){
            break
        }
        temp := cur
        cur = cur.Next
        temp.Next = ret
        ret = temp 
    }
    return ret
}

func reverseListRecursive(head *ListNode) *ListNode {
    if head == nil || head.Next == nil{
        return head
    }
    newHead := reverseListRecursive(head.Next)
    head.Next.Next = head
    head.Next = nil
        
    return newhead
}
```