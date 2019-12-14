using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Validation.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Extensions {
    [TestFixture]
    public class WithParameterInfo {
        public class MyClass {
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

        public class TestController {
            public void SimpleType(string foo) {}
            public void ComplexType(MyClass foo) {}
            public void TypeConvertedType(TypeConvertedClass foo) {}
            public void SimpleTypeFromUri([FromUri] string foo) {}
            public void ComplexTypeFromUri([FromUri] MyClass foo) {}
            public void TypeConvertedTypeFromUri([FromUri] TypeConvertedClass foo) {}
            public void SimpleTypeFromBody([FromBody] string foo) {}
            public void ComplexTypeFromBody([FromBody] MyClass foo) {}
            public void TypeConvertedTypeFromBody([FromBody] TypeConvertedClass foo) {}
        }


        [TestCase("SimpleType", true)]
        [TestCase("ComplexType", false)]
        [TestCase("TypeConvertedType", true)]
        [TestCase("SimpleTypeFromUri", true)]
        [TestCase("ComplexTypeFromUri", true)]
        [TestCase("TypeConvertedTypeFromUri", true)]
        [TestCase("SimpleTypeFromBody", false)]
        [TestCase("ComplexTypeFromBody", false)]
        [TestCase("TypeConvertedTypeFromBody", false)]
        public void ItShouldIdentifyParametersBoundFromUri(string methodName, bool expected) {
            var methodInfo = typeof(TestController).GetMethod(methodName);
            var arg = methodInfo.GetParameters().Single();
            arg.IsBoundFromUri()
               .Should()
               .Be(expected, $"{methodName} should {(expected ? string.Empty : "not ")}bind payload from Uri");
        }


        [TestCase("SimpleType", false)]
        [TestCase("ComplexType", true)]
        [TestCase("TypeConvertedType", false)]
        [TestCase("SimpleTypeFromUri", false)]
        [TestCase("ComplexTypeFromUri", false)]
        [TestCase("TypeConvertedTypeFromUri", false)]
        [TestCase("SimpleTypeFromBody", true)]
        [TestCase("ComplexTypeFromBody", true)]
        [TestCase("TypeConvertedTypeFromBody", true)]
        public void ItShouldIdentifyParametersBoundFromBody(string methodName, bool expected) {
            var methodInfo = typeof(TestController).GetMethod(methodName);
            var arg = methodInfo.GetParameters().Single();
            arg.IsBoundFromBody()
               .Should()
               .Be(expected, $"{methodName} should {(expected ? string.Empty : "not ")}bind payload from Body");
        }
    }
}
