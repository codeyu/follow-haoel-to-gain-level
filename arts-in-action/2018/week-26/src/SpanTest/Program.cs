using System;
using BenchmarkDotNet.Running;
namespace SpanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<StringConvert>();
        }
    }
}
