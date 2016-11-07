using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerInNameAndNoArgumentUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/HasControllerInName";
        private const string CurrentUrlWithTrailingSlash = "~/HasControllerInName/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.UsingConfiguration(HttpConfiguration).Should().MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url) {
            url.UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Post());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Put).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Put());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Delete).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<HasControllerInNameController>(controller => controller.Delete());
        }
    }
}
