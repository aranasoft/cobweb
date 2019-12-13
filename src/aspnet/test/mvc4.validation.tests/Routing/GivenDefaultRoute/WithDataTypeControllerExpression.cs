using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithDataTypeControllerExpression : GivenDefaultRoute {
        [Test]
        public void ItShouldMapControllerDefaultActionToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.Index();
            expression.Should().MapToUrl("/DataTypeInput");
        }

        [Test]
        public void ItShouldMapNoArgumentControllerActionUrlToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithNothing();
            expression.Should().MapToUrl("/DataTypeInput/WithNothing");
        }

        [Test]
        public void ItShouldMapNoArgumentPostControllerActionUrlToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.PostNothing();
            expression.Should().MapToUrl("/DataTypeInput/PostNothing");
        }

        [Test]
        public void ItShouldMapSpecifiedIntActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithInteger(42);
            expression.Should().MapToUrl("/DataTypeInput/WithInteger/42");
        }

        [Test]
        public void ItShouldMapEmptyNullableIntActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithNullInteger(null);
            expression.Should().MapToUrl("/DataTypeInput/WithNullInteger");
        }

        [Test]
        public void ItShouldMapSpecifiedNullIntActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithNullInteger(47);
            expression.Should().MapToUrl("/DataTypeInput/WithNullInteger/47");
        }

        [Test]
        public void ItShouldMapNullStringActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithString(null);
            expression.Should().MapToUrl("/DataTypeInput/WithString");
        }

        [Test]
        public void ItShouldMapEmptyStringActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithString(string.Empty);
            expression.Should().MapToUrl("/DataTypeInput/WithString");
        }

        [Test]
        public void ItShouldMapSpecifiedStringActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithString("SpecifiedString");
            expression.Should().MapToUrl("/DataTypeInput/WithString/SpecifiedString");
        }

        [Test, Ignore("Is this even possible?")]
        public void ItShouldMapSpecifiedDateTimeActionParameterToExpectedUrl() {
            var expected = 1.July(2015);
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithDateTime(expected);
            expression.Should().MapToUrl("/DataTypeInput/WithDateTime/2015-07-01");
        }

        [Test]
        public void ItShouldMapEmptyNullableDateTimeActionParameterToExpectedUrl() {
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithNullDateTime(null);
            expression.Should().MapToUrl("/DataTypeInput/WithNullDateTime");
        }

        [Test, Ignore("Is this even possible?")]
        public void ItShouldMapSpecifiedNullDateTimeActionParameterToExpectedUrl() {
            var expected = 1.July(2015);
            Expression<Func<DataTypeInputController, ActionResult>> expression =
                controller => controller.WithNullDateTime(expected);
            expression.Should().MapToUrl("/DataTypeInput/WithNullDateTime/2015-07-01");
        }
    }
}
