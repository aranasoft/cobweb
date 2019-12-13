using System;
using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerUrlAndContent : GivenDefaultRoute {
        private const string CurrentUrl = "~/Result";
        private const string CurrentUrlWithTrailingSlash = "~/Result/";
        private const string CurrentUrlWithId = "~/Result/5";
        private const string CurrentUrlWithIdAndTrailingSlash = "~/Result/5/";
        private const string Payload = "Hello";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.WithContent(Payload).UsingConfiguration(HttpConfiguration).Should().MapTo<ResultController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url) {
            url.UsingConfiguration(HttpConfiguration).Should().MapTo<ResultController>(controller => controller.Get());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration).Should().MapTo<ResultController>();
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapGetByIdToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration).Should().MapTo<ResultController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>(controller => controller.Get());
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapGetByIdToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>(controller => controller.Get(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Post)
               .WithJsonContent("{name: \"foo\"}")
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>(controller => controller.Post(new AnObject {Name = "foo"}));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldThrowOnMapPostToActionExpressionWithPropertyMismatch(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Post)
                                  .WithJsonContent("{name: \"bar\"}")
                                  .UsingConfiguration(HttpConfiguration)
                                  .Should()
                                  .MapTo<ResultController>(controller => controller.Post(new AnObject {Name = "foo"}));
            act.ShouldThrow<AssertionException>()
               .And.Message.Should()
               .StartWith("Expected member Name to be \"foo\", but \"bar\" differs near \"bar\"");
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put).UsingConfiguration(HttpConfiguration).Should().MapTo<ResultController>();
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapPutToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Put)
               .WithJsonContent("{name: \"foo\"}")
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>(controller => controller.Put(5, new AnObject {Name = "foo"}));
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldThrowOnMapPutToActionExpressionWithPropertyMismatch(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Put)
                                  .WithJsonContent("{name: \"bar\"}")
                                  .UsingConfiguration(HttpConfiguration)
                                  .Should()
                                  .MapTo<ResultController>(controller =>
                                                               controller.Put(5, new AnObject {Name = "foo"}));

            act.ShouldThrow<AssertionException>()
               .And.Message.Should()
               .StartWith("Expected member Name to be \"foo\", but \"bar\" differs near \"bar\"");
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>();
        }

        [TestCase(CurrentUrlWithId)]
        [TestCase(CurrentUrlWithIdAndTrailingSlash)]
        public void ItShouldMapDeleteToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ResultController>(controller => controller.Delete(5));
        }
    }
}
