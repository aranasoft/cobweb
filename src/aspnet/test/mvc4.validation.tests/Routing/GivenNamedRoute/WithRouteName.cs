using System;
using Cobweb.Testing.Mvc.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenNamedRoute {
    [TestFixture]
    public class WithRouteName : GivenNamedRoute {
        [Test]
        public void ItShouldMapRouteToUrl() {
            OutboundUrl.NamedRouteContext(ActualRouteName).Should().MapToUrl(ExpectedUrl);
        }

        [Test]
        public void ItShouldThrowUrlErrorOnMapRouteToIncorrectUrl() {
            Action act = () => OutboundUrl.NamedRouteContext(ActualRouteName).Should().MapToUrl("/Incorrect/Url");
            act.ShouldThrow<AssertionException>()
               .WithMessage("Expected outboundurl to resolve Url to \"/Incorrect/Url\", but found \"/Route/Name\".");
        }
    }
}
