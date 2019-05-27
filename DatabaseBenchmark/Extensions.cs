using System.Collections.Generic;

namespace DatabaseBenchmark
{
    public static class Extensions
    {
        public static List<T> ToListSignleItem<T>(this T source)
        {
            return new List<T> {source};
        }
    }
}