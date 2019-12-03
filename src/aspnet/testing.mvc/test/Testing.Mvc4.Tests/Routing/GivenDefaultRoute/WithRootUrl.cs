using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithRootUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/";

        [Test]
        public void ItShouldMapUrlToDefaultControllerType() {
            CurrentUrl.Should().MapTo<HomeController>();
        }

        [Test]
        public void ItShouldMapUrlToDefaultActionExpression() {
            CurrentUrl.Should().MapTo<HomeController>(controller => controller.Index());
        }
    }
}
