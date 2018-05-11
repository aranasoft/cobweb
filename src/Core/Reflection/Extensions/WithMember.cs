using System;
using Cobweb.Extentions.ObjectExtentions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cobweb.Reflection.Extensions {
    public static class WithMember {
        /// <summary>
        ///     Indicates whether one or more instance of <typeparamref name="TAttribute" /> is defined on the
        ///     <paramref name="member" />.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member to analyze.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>true if the attributeType is defined on this member; false otherwise.</returns>
        public static bool IsDefined<TAttribute>(this ICustomAttributeProvider member, bool inherit = true)
            where TAttribute : Attribute {
            return member.IsDefined(typeof(TAttribute), inherit);
        }

        /// <summary>
        ///     Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no
        ///     custom attributes of that type.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member to analyze.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>An array of <typeparamref name="TAttribute" /> representing custom attributes, or an empty array.</returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider member,
                                                                              bool inherit = true)
            where TAttribute : Attribute {
            return member.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        /// <summary>
        ///     Returns a custom attributes defined on this member, identified by type.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member to analyze.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>An instance of <typeparamref name="TAttribute" /> representing custom attributes, or null.</returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this ICustomAttributeProvider member,
                                                                bool inherit = true)
            where TAttribute : Attribute {
            return GetCustomAttributes<TAttribute>(member, inherit).IfExists(collection => collection.FirstOrDefault());
        }
    }
}
