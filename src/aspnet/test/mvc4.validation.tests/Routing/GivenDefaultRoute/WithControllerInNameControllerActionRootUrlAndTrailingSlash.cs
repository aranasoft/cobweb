using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerInNameControllerActionRootUrlAndTrailingSlash : GivenDefaultRoute {
        private const string CurrentUrl = "~/HasControllerInName/Other/";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<HasControllerInNameController>();
        }

        [Test]
        public void ItShouldMapUrlToSpecifiedActionExpression() {
            CurrentUrl.Should().MapTo<HasControllerInNameController>(controller => controller.Other());
        }
    }
}
