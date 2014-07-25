using System.Collections.Generic;

namespace CoreTest
{
    public static class CollectionExtensions
    {
        public static Iesi.Collections.Generic.ISet<T> AsSet<T>(this IEnumerable<T> collection) // todo: remove this + remove reference to Iesi.Collections
        {
            return (Iesi.Collections.Generic.ISet<T>)collection;
        }
    }
}