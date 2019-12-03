using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerInNameControllerRootUrlAndTrailingSlash : GivenDefaultRoute {
        private const string CurrentUrl = "~/HasControllerInName/";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<HasControllerInNameController>();
        }

        [Test]
        public void ItShouldMapUrlToDefaultActionExpression() {
            CurrentUrl.Should().MapTo<HasControllerInNameController>(controller => controller.Index());
        }
    }
}
