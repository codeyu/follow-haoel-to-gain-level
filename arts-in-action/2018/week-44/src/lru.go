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