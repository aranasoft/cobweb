using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Aranasoft.Cobweb.Mvc.Validation.Assertions;
using Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithControllerInNameControllerActionExpression : GivenDefaultRoute {
        private readonly Expression<Func<HasControllerInNameController, ActionResult>> _currentExpression =
            controller => controller.Other();

        [Test]
        public void ItShouldMapExpressionToControllerActionUrl() {
            _currentExpression.Should().MapToUrl("/HasControllerInName/Other");
        }
    }
}
