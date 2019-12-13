using Cobweb.Testing.Mvc.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrlWithPathInfo : GivenIgnoredRoute {
        private const string CurrentUrl = "~/image.jpg/ExtraPathInfo";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.Should().BeIgnoredRoute();
        }
    }
}
