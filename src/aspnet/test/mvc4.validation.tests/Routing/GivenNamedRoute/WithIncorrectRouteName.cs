using System;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenNamedRoute {
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
