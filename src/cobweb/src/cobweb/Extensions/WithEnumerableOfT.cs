using System;
using System.Collections.Generic;

namespace Aranasoft.Cobweb.Extensions;
/// <summary>
///     Extension methods for <see cref="IEnumerable{T}" />.
/// </summary>
public static class WithEnumerableOfT {
    /// <summary>
    ///     Execute a delegate on each item within an <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <typeparam name="T">The element type of the enumerable.</typeparam>
    /// <param name="items">The enumerable of items.</param>
    /// <param name="action">The delegate to execute for each item.</param>
    /// <remarks>This functionality is similar to Linq's ForEach method on List&lt;T&gt;</remarks>
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
        foreach (var item in items) {
            action(item);
        }
    }
}
