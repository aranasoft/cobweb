using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithAlternateControllerRootUrlAndTrailingSlash : GivenDefaultRoute {
        private const string CurrentUrl = "~/Other/";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<OtherController>();
        }

        [Test]
        public void ItShouldMapUrlToDefaultActionExpression() {
            CurrentUrl.Should().MapTo<OtherController>(controller => controller.Index());
        }
    }
}
