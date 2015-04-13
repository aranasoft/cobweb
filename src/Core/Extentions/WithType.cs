using System;

namespace Cobweb.Extentions {
    public static class WithType {
        /// <summary>
        /// Identify if a class derives from a specified generic base class.
        /// </summary>
        /// <param name="checkType">The derived type to analyse.</param>
        /// <param name="genericType">The generic base type to check against.</param>
        /// <returns>True if <paramref name="checkType" /> derives from <paramref name="genericType" />, otherwise False.</returns>
        public static bool IsAssignableToGeneric(this Type checkType, Type genericType) {
            if (genericType == null) {
                throw new ArgumentNullException("genericType");
            }
            if (! genericType.IsGenericType) {
                throw new ArgumentException("Type must be generic", "genericType");
            }

            if (checkType.IsGenericType && checkType.GetGenericTypeDefinition() == genericType) {
                return true;
            }

            return checkType.BaseType != null && IsAssignableToGeneric(checkType.BaseType, genericType);
        }
    }
}
