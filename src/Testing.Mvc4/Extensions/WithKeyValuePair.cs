using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cobweb.Testing.Mvc.Extensions {
    public static class WithKeyValuePair {
        /// <summary>
        ///     Convert a KeyValuePair into a NameObjectCollection object.
        /// </summary>
        /// <typeparam name="T">The type of value contained within the KeyValuePair.</typeparam>
        /// <param name="pairs">The KeyValuePair instances to convert.</param>
        /// <returns>A NameObjectCollection containing the keys and values from <paramref name="pairs" />.</returns>
        public static NameObjectCollectionBase ToNameObjectCollection<T>(
            this IEnumerable<KeyValuePair<string, T>> pairs) {
            var collection = new NameObjectCollection<T>();
            collection.AddRange(pairs);
            return collection;
        }
    }
}
