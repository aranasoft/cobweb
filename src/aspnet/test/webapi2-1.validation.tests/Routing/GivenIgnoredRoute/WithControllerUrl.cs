using Aranasoft.Cobweb.Http.Validation.Assertions;
using Aranasoft.Cobweb.Http.Validation.Tests.TestableTypes;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public class WithControllerUrl : GivenIgnoredRoute {
        private const string CurrentUrl = "~/primary";

        [Test]
        public void ItShouldMapUrlToDefaultControllerType() {
            CurrentUrl.UsingConfiguration(HttpConfiguration).Should().MapTo<PrimaryController>();
        }

        [Test]
        public void ItShouldMapUrlToDefaultActionExpression() {
            CurrentUrl.UsingConfiguration(HttpConfiguration)
                      .Should()
                      .MapTo<PrimaryController>(controller => controller.Get());
        }
    }
}
