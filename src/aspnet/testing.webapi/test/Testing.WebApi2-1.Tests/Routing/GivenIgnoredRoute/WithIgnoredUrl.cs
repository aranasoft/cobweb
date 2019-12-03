using Cobweb.Testing.WebApi.Assertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrl : GivenIgnoredRoute {
        private const string CurrentUrl = "~/primary/image.jpg";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.UsingConfiguration(HttpConfiguration).Should().BeIgnoredRoute();
        }
    }
}
