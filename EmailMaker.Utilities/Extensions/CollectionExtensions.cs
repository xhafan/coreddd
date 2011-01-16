using System;
using System.Collections.Generic;

namespace EmailMaker.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        public static void Each<T>(this IEnumerable<T> objects, Action<T> action)
        {
            foreach (var obj in objects)
            {
                action(obj);
            }
        }
    }
}
