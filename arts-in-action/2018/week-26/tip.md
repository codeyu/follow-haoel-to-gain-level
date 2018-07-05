## 关于 .NET 中新的数据类型 Span<T>
* System.Span<T> is a new value type at the heart of .NET. It enables the representation of contiguous regions of arbitrary memory, regardless of whether that memory is associated with a managed object, is provided by native code via interop, or is on the stack. And it does so while still providing safe access with performance characteristics like that of arrays.

* 所有涉及到IO操作以及内存操作的方法都会因使用Span<T>以及它的线程安全结构Memory<T>而获得巨大的性能提升

* 测试代码：[String Convert To Int](src/SpanTest)

* 参考文章：

https://medium.com/@antao.almada/how-to-use-span-t-and-memory-t-c0b126aae652

https://msdn.microsoft.com/en-us/magazine/mt814808.aspx

https://github.com/dotnet/corefxlab/blob/master/docs/specs/span.md

https://channel9.msdn.com/Events/Connect/2017/T125

http://adamsitnik.com/Span/

https://www.codemag.com/Article/1807051/Introducing-.NET-Core-2.1-Flagship-Types-Span-T-and-Memory-T


