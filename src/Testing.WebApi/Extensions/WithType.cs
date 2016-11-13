using System;
using System.ComponentModel;

namespace Cobweb.Testing.WebApi.Extensions
{
    public static class WithType {
        public static bool DefaultsToBoundFromUri(this Type type) {
            return type.HasSimpleType() || type.HasStringConverter();
        }

        public static bool DefaultsToBoundFromBody(this Type type) {
            return !type.DefaultsToBoundFromUri();
        }

        private static bool HasSimpleType(this Type type) {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null) {
                type = underlyingType;
            }

            return type.IsPrimitive ||
                   type == typeof(string) ||
                   type == typeof(DateTime) ||
                   type == typeof(decimal) ||
                   type == typeof(Guid) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan);
        }

        private static bool HasStringConverter(this Type type) {
            return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
        }
    }
}
