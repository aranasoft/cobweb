using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a ControllerActionExpression is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class ControllerActionExpressionAssertions<TController> :
        ReferenceTypeAssertions<Expression<Func<TController, ActionResult>>,
            ControllerActionExpressionAssertions<TController>> where TController : Controller {
        public ControllerActionExpressionAssertions(Expression<Func<TController, ActionResult>> value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "outboundurl"; }
        }

        /// <summary>
        ///     Asserts that a ControllerActionExpression maps to a specified url.
        /// </summary>
        /// <param name="expectedUrl">The expected url</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ControllerActionExpressionAssertions<TController>> MapToUrl(string expectedUrl,
                                                                                         string because = "",
                                                                                         params object[] reasonArgs) {
            var generatedUrl = Subject.RouteContext().GetUrl(HelperFactory.UrlHelper());

            var isMatchingUrl =
                string.Compare(generatedUrl, expectedUrl, StringComparison.InvariantCultureIgnoreCase) == 0;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(isMatchingUrl)
                   .FailWith("Expected {context:outboundurl} to resolve Url to {0}{reason}, but found {1}.",
                             expectedUrl,
                             generatedUrl);

            return new AndConstraint<ControllerActionExpressionAssertions<TController>>(this);
        }
    }
}
