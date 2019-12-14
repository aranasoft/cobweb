using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrl : GivenIgnoredRoute {
        private const string CurrentUrl = "~/image.jpg";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.Should().BeIgnoredRoute();
        }
    }
}
