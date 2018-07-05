using System;
using BenchmarkDotNet.Attributes;
namespace SpanTest
{
    [MemoryDiagnoser]
    public class StringConvert
    {
        [Benchmark]
        public void ConvertByParse()
        {
            var content = "content-length:123";
            for (int i = 0; i < 1000000; i++)
            {
                var ret = int.Parse(content.Substring(15));
            }
        }

        [Benchmark]
        public void ConvertByCustom()
        {
            var content = "content-length:123";
            for (int i = 0; i < 1000000; i++)
            {
                var ret = content.Substring(15).ParseToInt();
            }
        }

        [Benchmark]
        public void ConvertBySpan()
        {
            var content = "content-length:123";
            var span = content.ToCharArray().AsSpan();    
            for (var i = 0; i < 1000000; i++)
            {
                var ret = span.Slice(15).ParseToInt();
            }
        }
    }
}