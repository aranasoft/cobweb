using Cobweb.Testing.Mvc.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrl : GivenIgnoredRoute {
        private const string CurrentUrl = "~/image.jpg";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.Should().BeIgnoredRoute();
        }
    }
}
