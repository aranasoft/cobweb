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
            url.Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould405(string url) {
            Action act = () => url.AsRequest().GetActionDescriptor();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapDeleteToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Delete).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould405OnDelete(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Delete).GetActionDescriptor();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould405OnGet(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Get).GetActionDescriptor();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPostToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Post).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould405OnPost(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Post).GetActionDescriptor();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapPutToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Put).Should().MapTo<EmptyController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould405OnPut(string url) {
            Action act = () => url.WithHttpMethod(HttpMethod.Put).GetActionDescriptor();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }
    }
}
