using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreUtils.Extensions
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

        public static void Each<T>(this IEnumerable<T> objects, Action<int, T> action)
        {
            var objectList = objects.ToList();
            for (var i = 0; i < objectList.Count; i++)
            {
                action(i, objectList[i]);
            }
        }
    }
}
