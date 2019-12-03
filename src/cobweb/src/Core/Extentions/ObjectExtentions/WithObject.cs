using System;

namespace Cobweb.Extentions.ObjectExtentions {
    public static class WithObject {
        /// <summary>
        ///     Returns the result of <paramref name="delegate" /> if <paramref name="object" /> is not null.
        ///     Otherwise, <paramref name="default" /> is returned.
        /// </summary>
        /// <example>
        ///     This method may be chained to reduce subsequent null checks.
        ///     <code> parentObject.IfExists(parent => parent.Children).IfExists(children => children.Count(), 0) </code>
        /// </example>
        /// <typeparam name="T">The type of the <paramref name="object" />.</typeparam>
        /// <typeparam name="TResult">The return type of the <paramref name="delegate" />.</typeparam>
        /// <param name="object">The object that may be null</param>
        /// <param name="delegate">The delegate to execute on the object if the object is not null.</param>
        /// <param name="default">The value to return if <paramref name="object" /> is null.</param>
        /// <returns>
        ///     The result of the <paramref name="delegate" /> or, if the <paramref name="object" /> is null,
        ///     <paramref name="default" />.
        /// </returns>
        public static TResult IfExists<T, TResult>(this T @object,
                                                   Func<T, TResult> @delegate,
                                                   TResult @default = default(TResult))
            where T : class {
            return @object != null ? @delegate(@object) : @default;
        }
    }
}
