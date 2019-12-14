using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithDefaultControllerActionExpression : GivenDefaultRoute {
        private readonly Expression<Func<HomeController, ActionResult>> _currentExpression =
            controller => controller.Index();

        [Test]
        public void ItShouldMapExpressionToRootUrl() {
            _currentExpression.Should().MapToUrl("/");
        }
    }
}
