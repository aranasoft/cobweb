using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
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
