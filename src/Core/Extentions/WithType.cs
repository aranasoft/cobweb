using System;
using System.Linq;

namespace Cobweb.Extentions {
    public static class WithType {
        /// <summary>
        ///     Identify if a class derives from a specified generic base class.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <param name="genericBaseType">The generic base type to check against.</param>
        /// <returns>true if <paramref name="currentType" /> derives from <paramref name="genericBaseType" />, otherwise false.</returns>
        public static bool IsAssignableToGeneric(this Type currentType, Type genericBaseType) {
            if (genericBaseType == null) {
                throw new ArgumentNullException(nameof(genericBaseType));
            }

            if (!genericBaseType.IsGenericType) {
                throw new ArgumentException("Type must be generic", nameof(genericBaseType));
            }

            if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == genericBaseType) {
                return true;
            }

            if (!genericBaseType.IsInterface) {
                return currentType.BaseType != null && IsAssignableToGeneric(currentType.BaseType, genericBaseType);
            }

            return currentType.GetInterfaces()
                              .Any(interfaceType => IsAssignableToGeneric(interfaceType, genericBaseType));
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned to an instance of the specified Type.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <param name="baseType">The type to compare with the current type.</param>
        /// <returns>true if <paramref name="currentType" /> derives from <paramref name="baseType" />, otherwise false.</returns>
        public static bool IsAssignableTo(this Type currentType, Type baseType) {
            if (baseType == null) {
                throw new ArgumentNullException(nameof(baseType));
            }

            return baseType.IsAssignableFrom(currentType);
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned to an instance of the specified Type.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <typeparam name="TBase">The type to compare with the current type.</typeparam>
        /// <returns>true if <paramref name="currentType" /> derives from <typeparamref name="TBase" />, otherwise false.</returns>
        public static bool IsAssignableTo<TBase>(this Type currentType) {
            return IsAssignableTo(currentType, typeof(TBase));
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned from an instance of the specified Type.
        /// </summary>
        /// <param name="baseType">The base type to analyze.</param>
        /// <typeparam name="TDerived">The type to compare with the current type.</typeparam>
        /// <returns>true if <typeparamref name="TDerived" /> derives from <paramref name="baseType" />, otherwise false.</returns>
        public static bool IsAssignableFrom<TDerived>(this Type baseType) {
            return IsAssignableTo(typeof(TDerived), baseType);
        }

        /// <summary>
        ///     Determines whether an variable of the specified <paramref name="type" /> can be assigned null.
        /// </summary>
        /// <param name="type">The specified type.</param>
        /// <returns>true if a variable of the specified type can be assigned null, otherwise false</returns>
        public static bool CanBeNull(this Type type) {
            return !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);
        }
    }
}
