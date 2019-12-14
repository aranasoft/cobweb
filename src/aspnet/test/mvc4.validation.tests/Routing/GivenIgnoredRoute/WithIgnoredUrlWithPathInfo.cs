using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrlWithPathInfo : GivenIgnoredRoute {
        private const string CurrentUrl = "~/image.jpg/ExtraPathInfo";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.Should().BeIgnoredRoute();
        }
    }
}
