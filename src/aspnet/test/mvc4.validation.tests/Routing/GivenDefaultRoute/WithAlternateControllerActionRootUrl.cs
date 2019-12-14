using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithAlternateControllerActionRootUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/Other/Other";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<OtherController>();
        }

        [Test]
        public void ItShouldMapUrlToSpecifiedActionExpression() {
            CurrentUrl.Should().MapTo<OtherController>(controller => controller.Other());
        }
    }
}
