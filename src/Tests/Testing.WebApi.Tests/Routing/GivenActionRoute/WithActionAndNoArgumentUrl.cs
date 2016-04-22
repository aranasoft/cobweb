using System;
using System.Net.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public class WithActionAndNoArgumentUrl : GivenActionRoute {
        private const string CurrentUrl = "~/Action/OtherGet";
        private const string CurrentUrlWithTrailingSlash = "~/Action/OtherGet/";

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToActionExpression(string url)
        {
            url.Should().MapTo<ActionController>(controller => controller.OtherGet());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapToControllerType(string url)
        {
            url.Should().MapTo<ActionController>();
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToActionExpression(string url)
        {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<ActionController>(controller => controller.OtherGet());
        }

        [TestCase(CurrentUrl)]
        [TestCase(CurrentUrlWithTrailingSlash)]
        public void ItShouldMapGetToControllerType(string url)
        {
            url.WithHttpMethod(HttpMethod.Get).Should().MapTo<ActionController>();
        }
    }
}
