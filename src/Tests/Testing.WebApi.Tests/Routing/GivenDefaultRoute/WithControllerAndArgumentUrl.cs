using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerAndArgumentUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/Primary";
        private const string CurrentUrlWithTrailingSlash = "~/Primary/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.Should().MapTo<PrimaryController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url)
        {
            url.Should().MapTo<PrimaryController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<PrimaryController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<PrimaryController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post).Should().MapTo<PrimaryController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Post).Should().MapTo<PrimaryController>(controller => controller.Post());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put).Should().MapTo<PrimaryController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Put).Should().MapTo<PrimaryController>(controller => controller.Put());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete).Should().MapTo<PrimaryController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Delete).Should().MapTo<PrimaryController>(controller => controller.Delete());
        }
    }
}
