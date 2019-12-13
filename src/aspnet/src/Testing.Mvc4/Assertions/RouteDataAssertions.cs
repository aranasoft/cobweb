using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Extentions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="RouteData" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class RouteDataAssertions : ReferenceTypeAssertions<RouteData, RouteDataAssertions> {
        public RouteDataAssertions(RouteData value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "RouteData"; }
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> is an ignored route.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteDataAssertions> BeIgnored(string because = "", params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:routedata} to be ignored{reason}, but {context:routedata} was <null>.");
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.RouteHandler.GetType().IsAssignableTo(typeof(StopRoutingHandler)))
                   .FailWith("Expected {context:routedata} to be ignored{reason}");

            return new AndConstraint<RouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> maps to a specified
        ///     <typeparamref name="TController">controller</typeparamref>.
        /// </summary>
        /// <typeparam name="TController">The type of Controller.</typeparam>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteDataAssertions> MapTo<TController>(string because = "", params object[] reasonArgs)
            where TController : IController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            Subject.Values.Should().MapTo<TController>(because, reasonArgs);

            return new AndConstraint<RouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> maps to a specified
        ///     <paramref name="expectedController">controller</paramref>.
        /// </summary>
        /// <param name="expectedController">The name of the controller.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteDataAssertions> MapToController(string expectedController,
                                                                  string because = "",
                                                                  params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            Subject.Values.Should().MapToController(expectedController, because, reasonArgs);

            return new AndConstraint<RouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> maps to a specified
        ///     <paramref name="expectedAction">action</paramref>.
        /// </summary>
        /// <param name="expectedAction">The name of the action.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteDataAssertions> MapToAction(string expectedAction,
                                                              string because = "",
                                                              params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            Subject.Values.Should().MapToAction(expectedAction, because, reasonArgs);

            return new AndConstraint<RouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> maps to a specified <paramref name="action">action</paramref>.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="action">The action to call on <typeparamref name="TController" />.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        /// <remarks>Assertions are peformed against the specified controller, action, and action parameters.</remarks>
        public AndConstraint<RouteDataAssertions> MapTo<TController>(Expression<Func<TController, ActionResult>> action,
                                                                     string because = "",
                                                                     params object[] reasonArgs)
            where TController : IController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            Subject.Values.Should().MapTo(action, because, reasonArgs);

            return new AndConstraint<RouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteData">routeData</see> maps to a specified WebForms page.
        /// </summary>
        /// <param name="expectedVirtualPath">The ~/ based path to the WebForms page</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteDataAssertions> MapToPage(string expectedVirtualPath,
                                                            string because = "",
                                                            params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:routedata} to resolve VirtualPath to {0}{reason}, but {context:routedata} was <null>.",
                           expectedVirtualPath);
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.RouteHandler.GetType().IsAssignableTo(typeof(PageRouteHandler)))
                   .FailWith("Expected {context:routedata} to be handled by {0}{reason}, but found {1}",
                             typeof(PageRouteHandler).Name,
                             Subject.RouteHandler.GetType());

            var handler = Subject.RouteHandler as PageRouteHandler;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(
                       string.Compare(handler.VirtualPath,
                                      expectedVirtualPath,
                                      StringComparison.InvariantCulture) ==
                       0)
                   .FailWith("Expected {context:routedata} to resolve VirtualPath to {0}{reason}, but found {1}.",
                             expectedVirtualPath,
                             handler.VirtualPath);

            return new AndConstraint<RouteDataAssertions>(this);
        }
    }
}
