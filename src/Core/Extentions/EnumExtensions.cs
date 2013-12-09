using System;
using System.ComponentModel;
using System.Linq;

namespace Aranasoft.Cobweb.Extentions {
    public static class EnumExtensions {
        public static string GetDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var enumMemberInfo = type.GetMember(enumeration.ToString());

            if (!enumMemberInfo.Any()) return enumeration.ToString();

            var attributes = enumMemberInfo.First().GetCustomAttributes(typeof (DescriptionAttribute), false);
            if (attributes.Any())
                return ((DescriptionAttribute) attributes.First()).Description;

            return enumeration.ToString();
        }
    }
}