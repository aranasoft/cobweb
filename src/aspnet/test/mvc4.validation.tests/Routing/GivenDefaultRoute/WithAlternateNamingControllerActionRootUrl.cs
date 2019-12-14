using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithAlternateNamingControllerActionRootUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/AlternateNaming/ActionName";

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
