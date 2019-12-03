using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithRootUrl : GivenIgnoredRoute {
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
