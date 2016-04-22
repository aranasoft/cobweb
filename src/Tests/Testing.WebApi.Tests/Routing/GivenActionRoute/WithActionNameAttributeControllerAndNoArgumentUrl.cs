using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute
{
    [TestFixture]
    public class WithActionNameAttributeControllerAndNoArgumentUrl : GivenActionRoute {
        private const string CurrentUrl = "~/ActionNameAttribute/GetActionName";
        private const string CurrentUrlWithTrailingSlash = "~/ActionNameAttribute/GetActionName/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.Should().MapTo<ActionNameAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToRenamedActionExpression(string url) {
            url.Should().MapTo<ActionNameAttributeController>(controller => controller.GetWithAlternateActionName());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<ActionNameAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToRenamedActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .Should()
               .MapTo<ActionNameAttributeController>(controller => controller.GetWithAlternateActionName());
        }
    }
}
