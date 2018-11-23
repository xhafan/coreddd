using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreUtils.Extensions
{
    /// <summary>
    /// Collection extension methods.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Execute an action for each item in the collection.
        /// </summary>
        /// <typeparam name="TItem">An item type</typeparam>
        /// <param name="items">A collection of items</param>
        /// <param name="action">An item action</param>
        public static void Each<TItem>(this IEnumerable<TItem> items, Action<TItem> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Execute an action with an item index in the collection for each item in the collection.
        /// </summary>
        /// <typeparam name="TItem">An item type</typeparam>
        /// <param name="items">A collection of items</param>
        /// <param name="action">An item action with an index in the collection</param>
        public static void Each<TItem>(this IEnumerable<TItem> items, Action<int, TItem> action)
        {
            var itemList = items.ToList();
            for (var i = 0; i < itemList.Count; i++)
            {
                action(i, itemList[i]);
            }
        }

        /// <summary>
        /// Determines if the collection is empty.
        /// </summary>
        /// <typeparam name="TItem">An item type</typeparam>
        /// <param name="items">A collection of items</param>
        /// <returns>true when the collection is empty, otherwise returns false</returns>
        public static bool IsEmpty<TItem>(this IEnumerable<TItem> items)
        {
            return !items.Any();
        }

        /// <summary>
        /// Returns the second item from the collection.
        /// </summary>
        /// <typeparam name="TItem">An item type</typeparam>
        /// <param name="items">A collection of items</param>
        /// <returns>The second item from the collection</returns>
        public static TItem Second<TItem>(this IEnumerable<TItem> items)
        {
            return items.ElementAt(1);
        }

    }
}
