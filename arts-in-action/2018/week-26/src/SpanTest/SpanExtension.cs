using System;
namespace SpanTest
{
    public static class SpanExtension
    {
        public static int ParseToInt(this Span<char> rspan)
        {
            Int16 sign = 1;
            int num = 0;
            UInt16 index = 0;
            for (int idx = index; idx < rspan.Length; idx++)
            {
                ref char c = ref rspan[idx];
                num = (c - '0') + num * 10;
            }
            return num * sign;
        }

        public static int ParseToInt(this string code)
        {
            Int16 sign = 1;
            int num = 0;
            UInt16 index = 0;
            for (int idx = index; idx < code.Length; idx++)
            {
                char c = code[idx];
                num = (c - '0') + num * 10;
            }
            return num * sign;
        }
    }
}