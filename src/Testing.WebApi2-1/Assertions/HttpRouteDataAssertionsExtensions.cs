using System.Diagnostics;
using System.Web.Http.Routing;
using Cobweb.Extentions;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Cobweb.Testing.WebApi.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="IHttpRouteData" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public static class HttpRouteDataAssertionsExtensions {
        /// <summary>
        ///     Asserts that a <see cref="IHttpRouteData">routeData</see> is an ignored route.
        /// </summary>
        /// <param name="assertion">An <see cref="HttpRouteDataAssertions"/> context</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public static AndConstraint<HttpRouteDataAssertions> BeIgnored(this HttpRouteDataAssertions assertion,
                                                                       string because = "", params object[] reasonArgs) {
            if (ReferenceEquals(assertion.Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                                 "Expected {context:routedata} to be ignored{reason}, but {context:routedata} was <null>.");
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(assertion.Subject.Route.Handler.GetType().IsAssignableTo(typeof(StopRoutingHandler)))
                   .FailWith("Expected {context:routedata} to be ignored{reason}");

            return new AndConstraint<HttpRouteDataAssertions>(assertion);
        }
    }
}
