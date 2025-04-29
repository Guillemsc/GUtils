using System.Collections.Generic;

namespace GUtils.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the Dictionary.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> toAdd
            ) where TKey : notnull
        {
            foreach (KeyValuePair<TKey, TValue> item in toAdd)
            {
                dictionary.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the Dictionary.
        /// Similar to List <see cref="List{T}.AddRange"/>.
        /// </summary>
        public static void AddRange<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            IReadOnlyDictionary<TKey, TValue> toAdd
            ) where TKey : notnull
        {
            foreach (KeyValuePair<TKey, TValue> item in toAdd)
            {
                dictionary.Add(item.Key, item.Value);
            }
        }
    }
}
