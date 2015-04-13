using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Cobweb.Extentions {
    public static class WithEnum {
        public static string GetDescription(this Enum enumeration) {
            Type type = enumeration.GetType();
            MemberInfo[] enumMemberInfo = type.GetMember(enumeration.ToString());

            if (!enumMemberInfo.Any()) {
                return enumeration.ToString();
            }

            object[] attributes = enumMemberInfo.First().GetCustomAttributes(typeof (DescriptionAttribute), false);
            if (attributes.Any()) {
                return ((DescriptionAttribute) attributes.First()).Description;
            }

            return enumeration.ToString();
        }
    }
}
