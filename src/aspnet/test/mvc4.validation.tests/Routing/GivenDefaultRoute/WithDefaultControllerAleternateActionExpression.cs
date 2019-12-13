using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithDefaultControllerAleternateActionExpression : GivenDefaultRoute {
        private readonly Expression<Func<HomeController, ActionResult>> _currentExpression =
            controller => controller.Other();

        [Test]
        public void ItShouldMapExpressionToControllerActionUrl() {
            _currentExpression.Should().MapToUrl("/Home/Other");
        }
    }
}
