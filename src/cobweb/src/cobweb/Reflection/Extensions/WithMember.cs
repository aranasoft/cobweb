using System;
using System.Reflection;

namespace Aranasoft.Cobweb.Reflection.Extensions;
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
}
