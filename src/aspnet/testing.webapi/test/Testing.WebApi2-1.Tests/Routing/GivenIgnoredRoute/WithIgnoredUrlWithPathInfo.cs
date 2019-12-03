using Cobweb.Testing.WebApi.Assertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrlWithPathInfo : GivenIgnoredRoute {
        private const string CurrentUrl = "~/primary/image.jpg/ExtraPathInfo";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.UsingConfiguration(HttpConfiguration).Should().BeIgnoredRoute();
        }
    }
}
