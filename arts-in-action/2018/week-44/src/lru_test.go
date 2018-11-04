package main
import (
	"testing"
)

func TestLRU(t *testing.T) {
	
	obj := Constructor(128)
	

	for i := 0; i < 256; i++ {
		obj.Put(i, i)
	}
	
	
	for i := 0; i < 128; i++ {
		val := obj.Get(i)
		if val > 0 {
			t.Fatalf("should be evicted")
		}
	}
	for i := 128; i < 256; i++ {
		val := obj.Get(i)
		if val != i {
			t.Fatalf("should not be evicted")
		}
	}	

}

