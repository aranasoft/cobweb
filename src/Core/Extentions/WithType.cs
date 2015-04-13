using System;

namespace Cobweb.Extentions {
    public static class WithType {
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
