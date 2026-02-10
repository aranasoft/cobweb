using System;
using Aranasoft.Cobweb.Reflection.Extensions;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Tests.Reflection.Extensions;
public class GivenAClassWithAttributes {
    [Fact]
    public void ItShouldNotIdentifyNonExistantAttributes() {
        (typeof(ClassWithAttributes)).IsDefined<IgnoreAttribute>().Should().BeFalse();
    }

    [Fact]
    public void ItShouldIdentifyExistingAttributes() {
        (typeof(ClassWithAttributes)).IsDefined<CustomAttribute>().Should().BeTrue();
    }

    [Custom]
    public class ClassWithAttributes { }

    public class CustomAttribute : Attribute { }
    
    public class IgnoreAttribute : Attribute { }
}
