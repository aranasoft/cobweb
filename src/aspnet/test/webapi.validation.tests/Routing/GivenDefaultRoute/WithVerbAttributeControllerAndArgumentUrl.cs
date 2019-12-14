using System.Net.Http;
using Aranasoft.Cobweb.Http.Validation.Assertions;
using Aranasoft.Cobweb.Http.Validation.Tests.TestableTypes;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithVerbAttributeControllerAndArgumentUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/HttpMethodAttribute/5";
        private const string CurrentUrlWithTrailingSlash = "~/HttpMethodAttribute/5/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.UsingConfiguration(HttpConfiguration).Should().MapTo<HttpMethodAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url) {
            url.UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>(controller => controller.ExecuteMethodGet(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>(controller => controller.ExecuteMethodGet(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>(controller => controller.ExecuteMethodPost(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Put)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>(controller => controller.ExecuteMethodPut(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HttpMethodAttributeController>(controller => controller.ExecuteMethodDelete(5));
        }
    }
}
