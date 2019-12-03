using System;
using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithActionRootUrl : GivenDefaultRoute {
        private const string CurrentUrl = "~/Home/Other";

        [Test]
        public void ItShouldMapUrlToSpecifiedControllerType() {
            CurrentUrl.Should().MapTo<HomeController>();
        }

        [Test]
        public void ItShouldMapUrlToSpecifiedActionExpression() {
            CurrentUrl.Should().MapTo<HomeController>(controller => controller.Other());
        }
    }
}
