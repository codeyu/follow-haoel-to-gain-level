using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
namespace StringsAreEvil
{
    /// <summary>
    /// Original implementation.
    /// 
    /// Stats:-
    ///     Took: 8,797 ms
    ///     Allocated: 7,412,234 kb
    ///     Peak Working Set: 16,524 kb
    /// </summary>
    [MemoryDiagnoser]
    public class LineParserV01
    {
        private List<ValueHolder> list = new List<ValueHolder>();

        public void ParseLine(string line)
        {
            var parts = line.Split(',');
            if (parts[0] == "MNO")
            {
                var valueHolder = new ValueHolder(line);
                //list.Add(valueHolder);
            }
        }
        [Benchmark]
        public void ViaStreamReader()
        {
            using (StreamReader reader = File.OpenText(@"example-input.txt"))
            {
                try
                {
                    while (reader.EndOfStream == false)
                    {
                        ParseLine(reader.ReadLine());
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("File could not be parsed", exception);
                }
            }
        }
    }
}