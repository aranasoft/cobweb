using System;
using System.ComponentModel;
using Aranasoft.Cobweb.Http.Validation.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Extensions {
    [TestFixture]
    public class WithType {
        public class MyClass {
            public string Prop { get; set; }
        }

        public struct MyStruct {
            public string Prop { get; set; }
        }

        public class MyTypeConverter : TypeConverter {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }
        }

        [TypeConverter(typeof(MyTypeConverter))]
        public class TypeConvertedClass {
            public string Prop { get; set; }
        }

        [TypeConverter(typeof(MyTypeConverter))]
        public struct TypeConvertedStruct {
            public string Prop { get; set; }
        }

        // Simple Types and Enums
        [TestCase(typeof(string), true)]
        [TestCase(typeof(int), true)]
        [TestCase(typeof(decimal), true)]
        [TestCase(typeof(float), true)]
        [TestCase(typeof(StringComparison), true)]
        [TestCase(typeof(DateTime), true)]
        [TestCase(typeof(TimeSpan), true)]
        [TestCase(typeof(Guid), true)]

        // Nullable Simple Types and Enums
        [TestCase(typeof(int?), true)]
        [TestCase(typeof(decimal?), true)]
        [TestCase(typeof(float?), true)]
        [TestCase(typeof(StringComparison?), true)]
        [TestCase(typeof(DateTime?), true)]
        [TestCase(typeof(TimeSpan?), true)]
        [TestCase(typeof(Guid?), true)]

        // Complex Types with a Type Converter
        [TestCase(typeof(TypeConvertedClass), true)]
        [TestCase(typeof(TypeConvertedStruct), true)]

        // But not other Complex Types
        [TestCase(typeof(MyClass), false)]
        [TestCase(typeof(MyStruct), false)]
        public void ItShouldIdentifyTypesBoundFromUriByDefault(Type type, bool expected) {
            type.DefaultsToBoundFromUri().Should().Be(expected);
        }


        // Not Simple Types and Enums
        [TestCase(typeof(string), false)]
        [TestCase(typeof(int), false)]
        [TestCase(typeof(decimal), false)]
        [TestCase(typeof(float), false)]
        [TestCase(typeof(StringComparison), false)]
        [TestCase(typeof(DateTime), false)]
        [TestCase(typeof(TimeSpan), false)]
        [TestCase(typeof(Guid), false)]

        // Not Nullable Simple Types and Enums
        [TestCase(typeof(int?), false)]
        [TestCase(typeof(decimal?), false)]
        [TestCase(typeof(float?), false)]
        [TestCase(typeof(StringComparison?), false)]
        [TestCase(typeof(DateTime?), false)]
        [TestCase(typeof(TimeSpan?), false)]
        [TestCase(typeof(Guid?), false)]

        // Not Complex Types with a Type Converter
        [TestCase(typeof(TypeConvertedClass), false)]
        [TestCase(typeof(TypeConvertedStruct), false)]

        // Only other Complex Types
        [TestCase(typeof(MyClass), true)]
        [TestCase(typeof(MyStruct), true)]
        public void ItShouldIdentifyTypesBoundFromBodyByDefault(Type type, bool expected) {
            type.DefaultsToBoundFromBody().Should().Be(expected);
        }
    }
}
