using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.WebApi.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="IHttpRouteData" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class HttpRouteDataAssertions : ReferenceTypeAssertions<IHttpRouteData, HttpRouteDataAssertions> {
        public HttpRouteDataAssertions(IHttpRouteData value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => "IHttpRouteData";

        /// <summary>
        ///     Asserts that a <see cref="IHttpRouteData">routeData</see> maps to a specified
        ///     <typeparamref name="THttpController">controller</typeparamref>.
        /// </summary>
        /// <typeparam name="THttpController">The type of Controller.</typeparam>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpRouteDataAssertions> MapTo<THttpController>(string because = "",
                                                                             params object[] reasonArgs)
            where THttpController : IHttpController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            ((HttpRouteValueDictionary) Subject.Values).Should().MapTo<THttpController>(because, reasonArgs);

            return new AndConstraint<HttpRouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="IHttpRouteData">routeData</see> maps to a specified
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
        public AndConstraint<HttpRouteDataAssertions> MapToController(string expectedController, string because = "",
                                                                      params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            ((HttpRouteValueDictionary) Subject.Values).Should()
                                                       .MapToController(expectedController, because, reasonArgs);

            return new AndConstraint<HttpRouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="IHttpRouteData">routeData</see> maps to a specified
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
        public AndConstraint<HttpRouteDataAssertions> MapToAction(string expectedAction, string because = "",
                                                                  params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            ((HttpRouteValueDictionary) Subject.Values).Should().MapToAction(expectedAction, because, reasonArgs);

            return new AndConstraint<HttpRouteDataAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="IHttpRouteData">routeData</see> maps to a specified <paramref name="action">action</paramref>.
        /// </summary>
        /// <typeparam name="THttpController">The type of controller.</typeparam>
        /// <param name="action">The action to call on <typeparamref name="THttpController" />.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        /// <remarks>Assertions are peformed against the specified controller, action, and action parameters.</remarks>
        public AndConstraint<HttpRouteDataAssertions> MapTo<THttpController>(
            Expression<Func<THttpController, object>> action,
            string because = "",
            params object[] reasonArgs)
            where THttpController : IHttpController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routedata} to not be <null>{reason}.");
            }

            ((HttpRouteValueDictionary) Subject.Values).Should().MapTo(action, because, reasonArgs);

            return new AndConstraint<HttpRouteDataAssertions>(this);
        }
    }
}
