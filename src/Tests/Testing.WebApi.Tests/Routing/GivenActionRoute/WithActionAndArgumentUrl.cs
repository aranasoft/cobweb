using System;
using System.Collections.Generic;
using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public class WithActionAndArgumentUrl : GivenActionRoute {
        private const string CurrentUrl = "~/Action/OtherGet/5";
        private const string CurrentUrlWithTrailingSlash = "~/Action/OtherGet/5";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url) {
            url.Should().MapTo<ActionController>(controller => controller.OtherGet(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url) {
            url.Should().MapTo<ActionController>();
            Dictionary<WithActionAndArgumentUrl, GivenActionRoute> foo;
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<ActionController>(controller => controller.OtherGet(5));
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url) {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<ActionController>();
        }
    }
}
