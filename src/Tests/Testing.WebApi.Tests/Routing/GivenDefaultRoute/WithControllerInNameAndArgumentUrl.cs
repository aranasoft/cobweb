using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerInNameAndArgumentUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/HasControllerInName/5";
        private const string CurrentUrlWithTrailingSlash = "~/HasControllerInName/5/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url) {
            url.Should().MapTo<HasControllerInNameController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post).Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Post());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put).Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Put)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Put());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete).Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Delete());
        }
    }
}
