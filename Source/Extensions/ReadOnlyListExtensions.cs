using System;
using System.Collections.Generic;

namespace GUtils.Extensions
{
    public static class ReadOnlyListExtensions
    {
        /// <summary>
        /// Tries to get the element corresponding to the provided index.
        /// If index is out of bounds, it returns false.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGet<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            bool outsideBounds = index < 0 || list.Count <= index;

            if (outsideBounds)
            {
                item = default;
                return false;
            }

            item = list[index];
            return true;
        }

        /// <summary>
        /// Given an index, it clamps it between 0 and the array lenght - 1.
        /// </summary>
        public static int ClampIndex<T>(this IReadOnlyList<T> list, int index)
        {
            return Math.Clamp(index, 0, list.Count - 1);
        }

        /// <summary>
        /// Given an index, it clamps it between 0 and the array lenght - 1.
        /// Then, with the clamped index, it tries to get the element corresponding to that index.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGetClamped<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            index = list.ClampIndex(index);

            return list.TryGet(index, out item);
        }

        /// <summary>
        /// If item can be found inside the list, it returns its index.
        /// </summary>
        public static bool TryGetItemIndex<T>(this IReadOnlyList<T> list, T? item, out int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T checkingItem = list[i];

                if (!checkingItem.Equals(item))
                {
                    continue;
                }

                index = i;
                return true;
            }

            index = default;
            return false;
        }

        /// <summary>
        /// If item can be found inside the list, it returns its index.
        /// </summary>
        public static bool TryGetItemIndex<T>(this IReadOnlyList<T> list, Predicate<T> predicate, out int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T checkingItem = list[i];

                if (!predicate.Invoke(checkingItem))
                {
                    continue;
                }

                index = i;
                return true;
            }

            index = default;
            return false;
        }

        /// <summary>
        /// Checks if provided index is the last index of the list.
        /// </summary>
        /// <returns>True if it's the last index, false if it's not. If list is empty, returns false.</returns>
        public static bool IsLastIndex<T>(this IReadOnlyList<T> list, int index)
        {
            if (index < 0)
            {
                return false;
            }

            return list.Count - 1 == index;
        }
        
        /// <summary>
        /// Adds 1 to the provided index. If resulting index goes over array lenght, returns 0.
        /// </summary>
        public static int GetNextOrSmallestIndex<T>(this IReadOnlyList<T> array, int index)
        {
            int newIndex = index + 1;

            if (newIndex >= array.Count)
            {
                newIndex = 0;
            }

            return newIndex;
        }

        /// <summary>
        /// Substracts 1 to the provided index. If resulting index is smaller than zero, returns largest array index.
        /// </summary>
        public static int GetPreviousOrLargestIndex<T>(this IReadOnlyList<T> array, int index)
        {
            int newIndex = index - 1;

            if (newIndex < 0)
            {
                newIndex = array.Count - 1;
            }

            return newIndex;
        }
        
        /// <summary>
        /// Performs the specified action on each element of the List.
        /// </summary>
        public static void ForEach<T>(this IReadOnlyList<T> array, Action<T> action)
        {
            for (int i = 0; i < array.Count; i++)
            {
                T item = array[i];
                action.Invoke(item);
            }
        }

        public static bool IsOutsideBounds<T>(this IReadOnlyList<T> array, int index)
        {
            return index < 0 || index >= array.Count;
        }

        public static T? GetOrDefault<T>(this IReadOnlyList<T> array, int index)
        {
            bool outsideBounds = array.IsOutsideBounds(index);
            
            return outsideBounds ? default : array[index];
        }
    }
}
