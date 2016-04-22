using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Cobweb.Extentions;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Reflection.Extensions;
using Cobweb.Testing.WebApi.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.WebApi.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="HttpRouteData" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class HttpRequestMessageAssertions : ReferenceTypeAssertions<HttpRequestMessage, HttpRequestMessageAssertions> {
        public HttpRequestMessageAssertions(HttpRequestMessage value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => "HttpRequestMessage";

        /// <summary>
        ///     Asserts that a <see cref="HttpRequestMessage">requestmessage</see> maps to a specified
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
        public AndConstraint<HttpRequestMessageAssertions> MapTo<THttpController>(string because = "", params object[] reasonArgs)
            where THttpController : IHttpController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:requestmessage} to not be <null>{reason}.");
            }

            var expectedController = typeof(THttpController).Name;
            Subject.Should().MapToController(expectedController, because, reasonArgs);

            return new AndConstraint<HttpRequestMessageAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpRequestMessage">requestmessage</see> maps to a specified
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
        public AndConstraint<HttpRequestMessageAssertions> MapToController(string expectedController, string because = "",
                                                                  params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:requestmessage} to not be <null>{reason}.");
            }

            var controllerDescriptor = Subject.GetControllerDescriptor();
            var actualController = controllerDescriptor.IfExists(descriptor => descriptor.ControllerType.Name);

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(expectedController != null && expectedController.Equals(actualController))
                   .FailWith(
                             "Expected {context:requestmessage} to resolve to controller {0}{reason}, but controller was {1}.",
                             expectedController,
                             actualController
                );

            return new AndConstraint<HttpRequestMessageAssertions>(this);
        }

      

        /// <summary>
        ///     Asserts that a <see cref="HttpRequestMessage">requestmessage</see> maps to a specified
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
        public AndConstraint<HttpRequestMessageAssertions> MapToAction(string expectedAction, string because = "",
                                                              params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:requestmessage} to not be <null>{reason}.");
            }

            var actionDescriptor = Subject.GetActionDescriptor();
            var actualAction = actionDescriptor.IfExists(descriptor => descriptor.ActionName);

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(expectedAction != null && expectedAction.Equals(actualAction))
                   .FailWith(
                             "Expected {context:requestmessage} to resolve to action {0}{reason}, but action was {1}.",
                             expectedAction,
                             actualAction
                );

            return new AndConstraint<HttpRequestMessageAssertions>(this);
        }

     

        /// <summary>
        ///     Asserts that a <see cref="HttpRequestMessage">requestmessage</see> maps to a specified <paramref name="action">action</paramref>.
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
        public AndConstraint<HttpRequestMessageAssertions> MapTo<THttpController>(Expression<Action<THttpController>> action,
                                                                     string because = "",
                                                                     params object[] reasonArgs)
            where THttpController : IHttpController
        {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:requestmessage} to not be <null>{reason}.");
            }

            if (ReferenceEquals(Subject, null))
            {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to not be <null>{reason}.");
            }

            Subject.Should().MapTo<THttpController>(because, reasonArgs);
            Subject.Should().MapToAction(action.ActionName(), because, reasonArgs);

            //check parameters
            var methodArguments = action.GetMethodArgumentValues();
            foreach (var methodArgument in methodArguments)
            {
                var param = methodArgument.Key;
                var expectedValue = methodArgument.Value;
                var paramName = param.Name;

                if (param.ParameterType.CanBeNull())
                {
                    Subject.AsHttpRoute().Values.ToHttpRouteValueDictionary().Should().HaveOptionalParameter(paramName, expectedValue, param.ParameterType);
                }
                else {
                    Subject.AsHttpRoute().Values.ToHttpRouteValueDictionary().Should().HaveParameter(paramName, expectedValue, param.ParameterType);
                }
            }

            return new AndConstraint<HttpRequestMessageAssertions>(this);
        }
    }
}
