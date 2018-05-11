using Cobweb.Reflection.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Reflection.Extensions {
    [TestFixture]
    public class GivenAClassWithAttributes {
        [Test]
        public void ItShouldNotIdentifyNonExistantAttributes() {
            (typeof(GivenAClassWithAttributes)).IsDefined<IgnoreAttribute>().Should().BeFalse();
        }

        [Test]
        public void ItShouldIdentifyExistingAttributes() {
            (typeof(GivenAClassWithAttributes)).IsDefined<TestFixtureAttribute>().Should().BeTrue();
        }

        [Test]
        public void ItShouldNotRetrieveNonExistantAttributes() {
            (typeof(GivenAClassWithAttributes)).GetCustomAttributes<IgnoreAttribute>().Should().BeEmpty();
        }

        [Test]
        public void ItShouldRetrieveExistingAttributes() {
            var attrs = (typeof(GivenAClassWithAttributes)).GetCustomAttributes<TestFixtureAttribute>();
            attrs.Should().NotBeEmpty();
        }
    }
}
