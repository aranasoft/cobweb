using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
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
