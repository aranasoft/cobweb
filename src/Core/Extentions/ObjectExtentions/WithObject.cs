using System;

namespace Cobweb.Extentions.ObjectExtentions {
    public static class WithObject {
        public static TResult IfExists<T, TResult>(this T @object, Func<T, TResult> @delegate) where T : class {
            return IfExists(@object, @delegate, default(TResult));
        }

        public static TResult IfExists<T, TResult>(this T @object, Func<T, TResult> @delegate, TResult @default)
            where T : class {
            return @object != null ? @delegate(@object) : @default;
        }
    }
}
