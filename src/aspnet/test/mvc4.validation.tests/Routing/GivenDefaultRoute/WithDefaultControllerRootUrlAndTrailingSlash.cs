using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithDefaultControllerRootUrlAndTrailingSlash : GivenDefaultRoute {
        private const string CurrentUrl = "~/Home/";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<HomeController>();
        }

        [Test]
        public void ItShouldMapUrlToDefaultActionExpression() {
            CurrentUrl.Should().MapTo<HomeController>(controller => controller.Index());
        }
    }
}
