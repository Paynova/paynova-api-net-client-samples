using System;
using System.Collections.Generic;

namespace MvcSample.Core.Collections
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> src, Action<T> cb)
        {
            foreach (var i in src)
                cb(i);
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> src1, IEnumerable<T> src2)
        {
            foreach (var i in src1)
                yield return i;

            foreach (var i in src2)
                yield return i;
        }
    }
}