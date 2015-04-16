using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithAlternateNamingControllerActionExpression : GivenDefaultRoute {
        private readonly Expression<Func<AlternateNamingController, ActionResult>> _currentExpression =
            controller => controller.WithAlternateActionName();

        [Test]
        public void ItShouldMapExpressionToRenamedControllerActionUrl() {
            _currentExpression.Should().MapToUrl("/AlternateNaming/ActionName");
        }
    }
}
