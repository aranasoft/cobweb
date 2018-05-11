using System;
using Cobweb.Testing.Mvc.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenPageRoute {
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
