using System;
using System.IO;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace StringsAreEvil
{
    /// <summary>
    /// Stats:-
    ///     Took: 6,969 ms
    ///     Allocated: 4,288,215 kb
    ///     Peak Working Set: 16,640 kb
    ///
    /// Change:-
    ///     Use the orginal parts array
    /// </summary>
    [MemoryDiagnoser]
    public class LineParserV02
    {
        public void ParseLine(string line)
        {
            var parts = line.Split(',');
            if (parts[0] == "MNO")
            {
                var valueHolder = new ValueHolder(parts);
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