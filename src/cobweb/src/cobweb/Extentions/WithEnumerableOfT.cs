using System;
using System.Collections.Generic;

// ReSharper disable once IdentifierTypo
namespace Aranasoft.Cobweb.Extentions {
    [Obsolete("Use Aranasoft.Cobweb.Extensions.WithEnumerableOfT")]
    public static class WithEnumerableOfT {
        /// <summary>
        ///     Execute a delegate on each item within an IEnumerble.
        /// </summary>
        /// <typeparam name="T">The element type of the enumerable.</typeparam>
        /// <param name="items">The enumerable of items.</param>
        /// <param name="action">The delegate to execute for each item.</param>
        /// <remarks>This functionality is similar to Linq's ForEach method on List&lt;T&gt;</remarks>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithEnumerableOfT")]
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
            Extensions.WithEnumerableOfT.ForEach<T>(items, action);
        }
    }
}
