using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Validation.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithRootUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/";

        [Test]
        public void ItShould404() {
            Action act = () => CurrentUrl.UsingConfiguration(HttpConfiguration).SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void ItShould404OnDelete() {
            Action act =
                () =>
                    CurrentUrl.WithHttpMethod(HttpMethod.Delete)
                              .UsingConfiguration(HttpConfiguration)
                              .SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void ItShould404OnGet() {
            Action act =
                () => CurrentUrl.WithHttpMethod(HttpMethod.Get)
                                .UsingConfiguration(HttpConfiguration)
                                .SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void ItShould404OnPost() {
            Action act =
                () =>
                    CurrentUrl.WithHttpMethod(HttpMethod.Post)
                              .UsingConfiguration(HttpConfiguration)
                              .SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void ItShould404OnPut() {
            Action act =
                () => CurrentUrl.WithHttpMethod(HttpMethod.Put)
                                .UsingConfiguration(HttpConfiguration)
                                .SelectController();
            act.ShouldThrow<HttpResponseException>().And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
