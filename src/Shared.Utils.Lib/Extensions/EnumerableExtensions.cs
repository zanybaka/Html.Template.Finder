using System.Collections.Generic;
using System.Linq;

namespace Shared.Utils.Lib.Extensions
{
    public static class EnumerableExtensions
    {
        public static TSource Second<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(1).First();
        }

        public static TSource Third<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(2).First();
        }

        public static IEnumerable<TSource> TakeFrom<TSource>(this IEnumerable<TSource> source, int startIndex, int count)
        {
            return source.Skip(startIndex).Take(count);
        }
    }
}