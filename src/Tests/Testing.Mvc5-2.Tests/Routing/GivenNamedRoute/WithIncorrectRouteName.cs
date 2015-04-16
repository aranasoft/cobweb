using System;
using Cobweb.Testing.Mvc.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenNamedRoute {
    [TestFixture]
    public class WithIncorrectRouteName : GivenNamedRoute {
        [Test]
        public void ItShouldThrowHandlerErrorOnMapUrlToPage() {
            Action act = () => OutboundUrl.NamedRouteContext("incorrectRouteName").Should().MapToUrl(ExpectedUrl);
            act.ShouldThrow<ArgumentException>()
               .WithMessage("A route named 'incorrectRouteName' could not be found in the route collection.*");
        }
    }
}
