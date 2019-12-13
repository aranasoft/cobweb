using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cobweb.Testing.WebApi.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public class WithControllerRootUrl : GivenActionRoute {
        private const string CurrentUrl = "~/Action";
        private const string CurrentUrlWithTrailingSlash = "~/Action/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404(string url) {
            Action act = () => url.UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnDelete(string url) {
            Action act =
                () => url.WithHttpMethod(HttpMethod.Delete).UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnGet(string url) {
            Action act =
                () => url.WithHttpMethod(HttpMethod.Get).UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnPost(string url) {
            Action act =
                () => url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShould404OnPut(string url) {
            Action act =
                () => url.WithHttpMethod(HttpMethod.Put).UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
