using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenIgnoredRoute {
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
