using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Cobweb.Extentions {
    public static class WithEnum {
        /// <summary>
        ///     Returns the value of an enum element's DescriptionAttribute or the name of the element.
        /// </summary>
        /// <example>
        ///     <code> System.Reflection.BindingFlags.Public.GetDescription(); </code>
        /// </example>
        /// <remarks>Uses the value within a System.ComponentModel.DescriptionAttribute, if available.</remarks>
        /// <param name="enumeration">The enum element.</param>
        /// <returns>The identified element description.</returns>
        public static string GetDescription(this Enum enumeration) {
            Type type = enumeration.GetType();
            MemberInfo[] enumMemberInfo = type.GetMember(enumeration.ToString());

            if (!enumMemberInfo.Any()) {
                return enumeration.ToString();
            }

            object[] attributes = enumMemberInfo.First().GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any()) {
                return ((DescriptionAttribute) attributes.First()).Description;
            }

            return enumeration.ToString();
        }
    }
}
