using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BenchmarkDotNet.Running;
namespace StringsAreEvil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length>0 && args[0] == "g")
            {
                GenerateFile.Run();
                return;
            }
            var summary = BenchmarkRunner.Run<LineParserV01>();
            
        }
    }
}
