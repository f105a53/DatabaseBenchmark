using System;
using System.Collections.Generic;

namespace DatabaseBenchmark
{
    public static class Extensions
    {
        public static List<T> ToListSignleItem<T>(this T source)
        {
            return new List<T> {source};
        }

        public static void MustBeOne(this int i)
        {
            if (i != 1)
            {
                throw new Exception($"The result was not 1, but {i}");
            }
        }
    }
}