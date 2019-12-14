using Aranasoft.Cobweb.Http.Validation.Assertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithIgnoredUrl : GivenIgnoredRoute {
        private const string CurrentUrl = "~/primary/image.jpg";

        [Test]
        public void ItShouldIgnoreRoute() {
            CurrentUrl.UsingConfiguration(HttpConfiguration).Should().BeIgnoredRoute();
        }
    }
}
