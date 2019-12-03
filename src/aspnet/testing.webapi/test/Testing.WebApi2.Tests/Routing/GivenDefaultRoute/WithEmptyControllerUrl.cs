using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Extensions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithEmptyControllerUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/Empty";
        private const string CurrentUrlWithTrailingSlash = "~/Empty/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.UsingConfiguration(HttpConfiguration).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404(string url) {
            Action act = () => url.UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete)
               .UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnDelete(string url) {
            Action act =
                () => url.WithHttpMethod(HttpMethod.Delete).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnGet(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnPost(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put).UsingConfiguration(HttpConfiguration).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnPut(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Put).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.NotFound);
        }
    }
}
