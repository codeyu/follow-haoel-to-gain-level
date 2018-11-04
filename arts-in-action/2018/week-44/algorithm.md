## [146. LRU Cache](https://leetcode.com/problems/lru-cache/)

### 题目描述
Design and implement a data structure for [Least Recently Used (LRU) cache](https://en.wikipedia.org/wiki/Cache_replacement_policies#LRU). It should support the following operations: `get` and `put`.

`get(key)` - Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.
`put(key, value)` - Set or insert the value if the key is not already present. When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.

**Follow up:**
Could you do both operations in **O(1)** time complexity?

**Example:**
```
LRUCache cache = new LRUCache( 2 /* capacity */ );

cache.put(1, 1);
cache.put(2, 2);
cache.get(1);       // returns 1
cache.put(3, 3);    // evicts key 2
cache.get(2);       // returns -1 (not found)
cache.put(4, 4);    // evicts key 1
cache.get(1);       // returns -1 (not found)
cache.get(3);       // returns 3
cache.get(4);       // returns 4
```
### 解题思路
* go 中的实现方式为 list + map。
* list 用于刷新 entry 的生命周期，最前面的生存时间最长，最后面的最先删除。
* map 的 key 作为 entry 的 key ，map 的值存储 entry 的指针。 
* 每次 put/get 时，把 entry 移到 list 的最前面。  
* 如缓存数据大于Cache容量(capacity),则将最后面的 entry 删除。

### 实现代码

#### go
```go
package main
import (
	"container/list"
)



type LRUCache struct {
    capacity      int
	flushList *list.List
	items     map[int]*list.Element
}
// entry is used to hold a value in the flushList
type entry struct {
	key   int
	value int
}

func Constructor(capacity int) LRUCache {
    return LRUCache{
		capacity: capacity,
		flushList:   list.New(),
		items:       make(map[int]*list.Element),
	}
}


func (this *LRUCache) Get(key int) int {
    if ent, ok := this.items[key]; ok {
		this.flushList.MoveToFront(ent)
		return ent.Value.(*entry).value
	}
	return -1
}

// Add adds a value to the cache. 
func (this *LRUCache) Put(key int, value int)  {
    // Check for existing item
	if ent, ok := this.items[key]; ok {
		this.flushList.MoveToFront(ent)
        ent.Value.(*entry).value = value
        return
	}

	// Add new item
	ent := &entry{key, value}
	entry := this.flushList.PushFront(ent)
	this.items[key] = entry

	evict := this.flushList.Len() > this.capacity
	// Verify capacity not exceeded
	if evict {
		this.removeOldest()
	}
}

// removeOldest removes the oldest item from the cache.
func (this *LRUCache) removeOldest() {
	ent := this.flushList.Back()
	if ent != nil {
		this.removeElement(ent)
	}
}

// removeElement is used to remove a given list element from the cache
func (this *LRUCache) removeElement(e *list.Element) {
	this.flushList.Remove(e)
	kv := e.Value.(*entry)
	delete(this.items, kv.key)
}

/**
 * Your LRUCache object will be instantiated and called as such:
 * obj := Constructor(capacity);
 * param_1 := obj.Get(key);
 * obj.Put(key,value);
 */

```