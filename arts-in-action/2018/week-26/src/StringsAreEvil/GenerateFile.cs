using System;
using System.Collections.Generic;
using System.IO;

namespace StringsAreEvil
{
    public static class GenerateFile
    {
        public static void Run()
        {
            var dict = new Dictionary<string, int>
            {
                ["ABC"] = 1,
                ["BCD"] = 5,
                ["CDE"] = 25,
                ["DEF"] = 77,
                ["EFG"] = 4,
                ["FGH"] = 24,
                ["GHI"] = 4,
                ["HIJ"] = 144,
                ["IJK"] = 384,
                ["JKL"] = 276,
                ["KLM"] = 13,
                ["LMN"] = 2,
                ["MNO"] = 10036466,
                ["NOP"] = 279,
                ["OPQ"] = 9731
            };

            var random = new Random(252028800);
            var terms = new[] { 12, 24, 36, 48, 60 };
            var mileages = new[] { 6000, 10000, 15000, 20000, 25000, 30000, 40000, 50000 };

            using (var writer = File.CreateText(@"example-input.txt"))
            {
                foreach (var pair in dict)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        var elementId = random.Next(1, 10);
                        var next = random.Next(100000, 999999);
                        var term = terms[random.Next(0, terms.Length)];
                        var mileage = mileages[random.Next(0, mileages.Length)];
                        var line = $"{pair.Key},{elementId},{next},{term},{mileage},{random.Next(1, 100)}.{random.Next(1, 99)},,";
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}