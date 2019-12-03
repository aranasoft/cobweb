using System;
using Cobweb.Reflection.Extensions;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Reflection.Extensions {
    public class GivenAClassWithAttributes {
        [Fact]
        public void ItShouldNotIdentifyNonExistantAttributes() {
            (typeof(ClassWithAttributes)).IsDefined<IgnoreAttribute>().Should().BeFalse();
        }

        [Fact]
        public void ItShouldIdentifyExistingAttributes() {
            (typeof(ClassWithAttributes)).IsDefined<CustomAttribute>().Should().BeTrue();
        }

        [Fact]
        public void ItShouldNotRetrieveNonExistantAttributes() {
            (typeof(ClassWithAttributes)).GetCustomAttributes<IgnoreAttribute>().Should().BeEmpty();
        }

        [Fact]
        public void ItShouldRetrieveExistingAttributes() {
            var attrs = (typeof(ClassWithAttributes)).GetCustomAttributes<CustomAttribute>();
            attrs.Should().NotBeEmpty();
        }

        [Custom]
        public class ClassWithAttributes { }

        public class CustomAttribute : Attribute { }
        
        public class IgnoreAttribute : Attribute { }
    }
}
