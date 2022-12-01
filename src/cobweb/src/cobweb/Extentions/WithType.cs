using System;
using System.Linq;

// ReSharper disable once IdentifierTypo
namespace Aranasoft.Cobweb.Extentions {
    [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
    public static class WithType {
        /// <summary>
        ///     Identify if a class derives from a specified generic base class.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <param name="genericBaseType">The generic base type to check against.</param>
        /// <returns>true if <paramref name="currentType" /> derives from <paramref name="genericBaseType" />, otherwise false.</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
        public static bool IsAssignableToGeneric(this Type currentType, Type genericBaseType) {
            return Extensions.WithType.IsAssignableToGeneric(currentType, genericBaseType);
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned to an instance of the specified Type.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <param name="baseType">The type to compare with the current type.</param>
        /// <returns>true if <paramref name="currentType" /> derives from <paramref name="baseType" />, otherwise false.</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
        public static bool IsAssignableTo(this Type currentType, Type baseType) {
            return Extensions.WithType.IsAssignableTo(currentType, baseType);
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned to an instance of the specified Type.
        /// </summary>
        /// <param name="currentType">The derived type to analyze.</param>
        /// <typeparam name="TBase">The type to compare with the current type.</typeparam>
        /// <returns>true if <paramref name="currentType" /> derives from <typeparamref name="TBase" />, otherwise false.</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
        public static bool IsAssignableTo<TBase>(this Type currentType) {
            return Extensions.WithType.IsAssignableTo<TBase>(currentType);
        }

        /// <summary>
        ///     Determines whether an instance of the current System.Type can be assigned from an instance of the specified Type.
        /// </summary>
        /// <param name="baseType">The base type to analyze.</param>
        /// <typeparam name="TDerived">The type to compare with the current type.</typeparam>
        /// <returns>true if <typeparamref name="TDerived" /> derives from <paramref name="baseType" />, otherwise false.</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
        public static bool IsAssignableFrom<TDerived>(this Type baseType) {
            return Extensions.WithType.IsAssignableFrom<TDerived>(baseType);
        }

        /// <summary>
        ///     Determines whether an variable of the specified <paramref name="type" /> can be assigned null.
        /// </summary>
        /// <param name="type">The specified type.</param>
        /// <returns>true if a variable of the specified type can be assigned null, otherwise false</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithType")]
        public static bool CanBeNull(this Type type) {
            return Extensions.WithType.CanBeNull(type);
        }
    }
}
