using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenIgnoredRoute {
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
