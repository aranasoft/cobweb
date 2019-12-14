using System;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenPageRoute {
    [TestFixture]
    public class WithNonPageUrl : GivenPageRoute {
        private const string CurrentUrl = "~/Account/Info";

        [Test]
        public void ItShouldThrowHandlerErrorOnMapUrlToPage() {
            Action act = () => CurrentUrl.Should().MapToPage("~/Account/Info.aspx");
            act.ShouldThrow<AssertionException>()
               .WithMessage(
                   "Expected routedata to be handled by \"PageRouteHandler\", but found System.Web.Mvc.MvcRouteHandler");
        }
    }
}
