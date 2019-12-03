using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithAlternateNamingControllerActionRootUrlAndTrailingSlash : GivenDefaultRoute {
        private const string CurrentUrl = "~/AlternateNaming/ActionName/";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<AlternateNamingController>();
        }

        [Test]
        public void ItShouldMapUrlToSpecifiedRenamedActionExpression() {
            CurrentUrl.Should().MapTo<AlternateNamingController>(controller => controller.WithAlternateActionName());
        }
    }
}
