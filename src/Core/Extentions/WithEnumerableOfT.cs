using System;
using System.Collections.Generic;

namespace Cobweb.Extentions {
    public static class WithEnumerableOfT {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
            foreach (var item in items) {
                action(item);
            }
        }
    }
}
