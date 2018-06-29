using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StringsAreEvil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length>0 && args[0] == "g")
            {
                GenerateFile.Run();
            }
            AppDomain.MonitoringIsEnabled = true;

            var dict = new Dictionary<string, Action>
            {
                ["1"] = () =>
                {
                    Console.WriteLine("#1 ViaStreamReader");
                    ViaStreamReader(new LineParserV01());
                },
                
            };


#if DEBUG
            dict["14"]();
            Environment.Exit(0);
#endif

            if (args.Length == 1 && dict.ContainsKey(args[0]))
            {
                dict[args[0]]();
            }
            else
            {
                Console.WriteLine("Incorrect parameters");
                Environment.Exit(1);
            }

            Console.WriteLine($"Took: {AppDomain.CurrentDomain.MonitoringTotalProcessorTime.TotalMilliseconds:#,###} ms");
            Console.WriteLine($"Allocated: {AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize / 1024:#,#} kb");
            Console.WriteLine($"Peak Working Set: {Process.GetCurrentProcess().PeakWorkingSet64 / 1024:#,#} kb");

            for (var index = 0; index <= GC.MaxGeneration; index++)
            {
                Console.WriteLine($"Gen {index} collections: {GC.CollectionCount(index)}");
            }

            Console.WriteLine(Environment.NewLine);
        }

        private static void ViaStreamReader(ILineParser lineParser)
        {
            using (StreamReader reader = File.OpenText(@"example-input.txt"))
            {
                try
                {
                    while (reader.EndOfStream == false)
                    {
                        lineParser.ParseLine(reader.ReadLine());
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("File could not be parsed", exception);
                }
            }
        }

        private static void ViaFileStreamWithArrayPoolAndStringBuilder(ILineParser lineParser)
        {
            var sb = new StringBuilder();

            var charPool = ArrayPool<char>.Shared;

            using (var reader = File.OpenRead(@"example-input.txt"))
            {
                try
                {
                    bool endOfFile = false;
                    while (reader.CanRead)
                    {
                        sb.Clear();

                        while (endOfFile == false)
                        {
                            var readByte = reader.ReadByte();

                            if (readByte == -1)
                            {
                                endOfFile = true;
                                break;
                            }

                            var character = (char)readByte;

                            if (character == '\r')
                            {
                                continue;
                            }

                            if (character == '\n')
                            {
                                break;
                            }

                            sb.Append(character);
                        }

                        if (endOfFile)
                        {
                            break;
                        }

                        char[] rentedCharBuffer = charPool.Rent(sb.Length);

                        try
                        {
                            for (int index = 0; index < sb.Length; index++)
                            {
                                rentedCharBuffer[index] = sb[index];
                            }

                            lineParser.ParseLine(rentedCharBuffer);
                        }
                        finally
                        {
                            charPool.Return(rentedCharBuffer, true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("File could not be parsed", exception);
                }
            }
        }

        private static void ViaFileStreamWithStringBuilder(ILineParser lineParser)
        {
            var sb = new StringBuilder();

            using (var reader = File.OpenRead(@"example-input.txt"))
            {
                try
                {
                    bool endOfFile = false;
                    while (reader.CanRead)
                    {
                        sb.Clear();

                        while (endOfFile == false)
                        {
                            var readByte = reader.ReadByte();

                            if (readByte == -1)
                            {
                                endOfFile = true;
                                break;
                            }

                            var character = (char)readByte;

                            if (character == '\r')
                            {
                                continue;
                            }

                            if (character == '\n')
                            {
                                break;
                            }

                            sb.Append(character);
                        }

                        if (endOfFile)
                        {
                            break;
                        }

                        lineParser.ParseLine(sb);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("File could not be parsed", exception);
                }
            }
        }

        private static void ViaFileStreamWithStringBuilderWithCustomBufferSize(ILineParser lineParser, int bufferSize)
        {
            var sb = new StringBuilder();

            using (var reader = new FileStream(@"example-input.txt", FileMode.Open, FileAccess.Read, FileShare.None, bufferSize))
            {
                try
                {
                    bool endOfFile = false;
                    while (reader.CanRead)
                    {
                        sb.Clear();

                        while (endOfFile == false)
                        {
                            var readByte = reader.ReadByte();

                            if (readByte == -1)
                            {
                                endOfFile = true;
                                break;
                            }

                            var character = (char)readByte;

                            if (character == '\r')
                            {
                                continue;
                            }

                            if (character == '\n')
                            {
                                break;
                            }

                            sb.Append(character);
                        }

                        if (endOfFile)
                        {
                            break;
                        }

                        lineParser.ParseLine(sb);
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
