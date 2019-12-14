using System;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenPageRoute {
    [TestFixture]
    public class WithPageUrl : GivenPageRoute {
        private const string CurrentUrl = "~/Forms/Account/Info";

        [Test]
        public void ItShouldMapUrlToPage() {
            CurrentUrl.Should().MapToPage("~/Account/Info.aspx");
        }

        [Test]
        public void ItShouldThrowUrlErrorOnMapUrlToIncorrectPage() {
            Action act = () => CurrentUrl.Should().MapToPage("~/Account/WrongInfo.aspx");
            act.ShouldThrow<AssertionException>()
               .WithMessage(
                   "Expected routedata to resolve VirtualPath to \"~/Account/WrongInfo.aspx\", but found \"~/Account/Info.aspx\".");
        }
    }
}
