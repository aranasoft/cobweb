using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public class WithActionNameAttributeControllerAndArgumentUrl : GivenActionRoute {
        private const string CurrentUrl = "~/ActionNameAttribute/GetActionName/5";
        private const string CurrentUrlWithTrailingSlash = "~/ActionNameAttribute/GetActionName/5/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.UsingConfiguration(HttpConfiguration).Should().MapTo<ActionNameAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToRenamedActionExpression(string url) {
            url.UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ActionNameAttributeController>(controller => controller.GetWithAlternateActionName(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ActionNameAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToRenamedActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ActionNameAttributeController>(controller => controller.GetWithAlternateActionName(5));
        }
    }
}
