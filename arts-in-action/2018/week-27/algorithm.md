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
遍历解法：
* 首先，声明两个指针分别作为新链表头（newHead）和当前工作链表头（cur），newHead指向nil，cur指向旧链表的头
* 然后遍历旧链表，依次将旧链表的后续节点（cur.Next）添加到新链表（newHead）
* 同时新的链表头（newHead）指向当前节点（cur）作为头
* 遍历过程中要声明临时指针暂存后续节点。在下次遍历前当前节点（cur）指向后续节点
* 当遍历完成时，即实现逆序

递归解法：
* 递归最后一层递归出栈时处理的是最后一个节点，即新旧头都指向最后一个节点
* 新链表头永远指向链尾，旧头反转的指向为：head.Next.Next = head,
* 同时要断开当前指针：head.Next = nil
* todo
参考：https://blog.csdn.net/fx677588/article/details/72357389
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
    var newHead *ListNode = nil
    cur := head
    for{
        if(cur == nil){
            break
        }
        temp := cur.Next
        cur.Next = newHead
        newHead = cur
        cur = temp 
    }
    return newHead
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