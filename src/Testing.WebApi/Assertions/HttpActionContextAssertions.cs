using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Controllers;
using Cobweb.Extentions;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Reflection.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.WebApi.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="HttpActionContext" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class HttpActionContextAssertions :
        ReferenceTypeAssertions<HttpActionContext, HttpActionContextAssertions> {
        public HttpActionContextAssertions(HttpActionContext value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => "HttpActionContext";

        /// <summary>
        /// Asserts that a <see cref="HttpActionContext">actionContext</see> ActionArguments dictionary does not contain any items.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveNoArguments(string because = "",
                                                                          params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to be empty{reason}, but found {0}.", Subject);
            }

            if (ReferenceEquals(Subject.ActionArguments, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to be empty{reason}, but found {0}.",
                                 Subject.ActionArguments);
            }

            Execute.Assertion
                   .ForCondition(!Subject.ActionArguments.Any())
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected {context:actioncontext} to not have any arguments{reason}, but found {0}.",
                             Subject.ActionArguments.Count);

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        /// Asserts that a <see cref="HttpActionContext">actionContext</see> ActionArguments dictionary contains at least 1 item.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveArguments(string because = "",
                                                                        params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} not to be empty{reason}, but found {0}.", Subject);
            }

            if (ReferenceEquals(Subject.ActionArguments, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} not to be empty{reason}, but found {0}.",
                                 Subject.ActionArguments);
            }

            Execute.Assertion
                   .ForCondition(Subject.ActionArguments.Any())
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected {context:actioncontext} to have one or more items{reason}, but found none.");

            return new AndConstraint<HttpActionContextAssertions>(this);
        }


        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> ActionArguments has a specified argument.
        /// </summary>
        /// <param name="key">The action argument key.</param>
        /// <param name="expectedValue">The expected action argument value.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveEquivalentActionArgument(string key,
                                                                                       string expectedValue,
                                                                                       string because = "",
                                                                                       params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:actioncontext} to to have argument {0}{reason}, but {context:actioncontext} was <null>.",
                           key);
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.ActionArguments.ContainsKey(key))
                   .FailWith(
                       "Expected {context:actioncontext} to to have argument {0}{reason}, but argument {0} was not found in {context:actioncontext}.",
                       key
                   );

            var actualValue = Subject.ActionArguments.ContainsKey(key) &&
                              !string.IsNullOrEmpty(Subject.ActionArguments[key].IfExists(val => val.ToString()))
                ? Subject.ActionArguments[key].IfExists(val => val.ToString())
                : null;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(
                       string.Compare(expectedValue,
                                      actualValue,
                                      StringComparison.InvariantCultureIgnoreCase) ==
                       0)
                   .FailWith(
                       "Expected {context:actioncontext} to to have argument {0} with value {1}{reason}, but value was {2}.",
                       key,
                       expectedValue,
                       actualValue
                   );

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> has a specified route parameter.
        /// </summary>
        /// <typeparam name="TValue">The type of <paramref name="expectedValue" /></typeparam>
        /// <param name="key">The route parameter key.</param>
        /// <param name="expectedValue">The expected route parameter value.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveActionArgument<TValue>(string key,
                                                                                     TValue expectedValue,
                                                                                     string because = "",
                                                                                     params object[] reasonArgs)
            where TValue : class {
            return HaveActionArgument(key, expectedValue, typeof(TValue), because, reasonArgs);
        }


        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> has a specified action argument.
        /// </summary>
        /// <param name="key">The action argument key.</param>
        /// <param name="expectedValue">The expected action argument value.</param>
        /// <param name="expectedValueType">The type of <paramref name="expectedValue" /></param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveActionArgument(string key,
                                                                             object expectedValue,
                                                                             Type expectedValueType,
                                                                             string because = "",
                                                                             params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:actioncontext} to to have argument {0}{reason}, but {context:actioncontext} was <null>.",
                           key);
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.ActionArguments.ContainsKey(key))
                   .FailWith(
                       "Expected {context:actioncontext} to to have argument {0}{reason}, but argument {0} was not found in {context:actioncontext}.",
                       key
                   );


            object actualValue;
            Subject.ActionArguments.TryGetValue(key, out actualValue);

            actualValue.Should().BeOfType(Nullable.GetUnderlyingType(expectedValueType) ?? expectedValueType);
            actualValue.ShouldBeEquivalentTo(expectedValue, options => options.RespectingRuntimeTypes());

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> has an optional specified action argument.
        /// </summary>
        /// <typeparam name="TValue">The type of <paramref name="expectedValue" /></typeparam>
        /// <param name="key">The action argument key.</param>
        /// <param name="expectedValue">The expected action argument value/</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveOptionalActionArgument<TValue>(string key,
                                                                                             TValue expectedValue,
                                                                                             string because = "",
                                                                                             params object[]
                                                                                                 reasonArgs)
            where TValue : class {
            return HaveOptionalActionArgument(key, expectedValue, typeof(TValue), because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> has an optional specified action argument.
        /// </summary>
        /// <param name="key">The action argument key.</param>
        /// <param name="expectedValue">The expected action argument value/</param>
        /// <param name="expectedValueType">The type of <paramref name="expectedValue" /></param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<HttpActionContextAssertions> HaveOptionalActionArgument(string key,
                                                                                     object expectedValue,
                                                                                     Type expectedValueType,
                                                                                     string because = "",
                                                                                     params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:actioncontext} to to have optional argument {0}{reason}, but {context:actioncontext} was <null>.",
                           key);
            }

            if (!ReferenceEquals(expectedValue, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .ForCondition(Subject.ActionArguments.ContainsKey(key))
                       .FailWith(
                           "Expected {context:actioncontext} to to have argument {0} with value {1}{reason}, but argument {0} was not found in {context:actioncontext}.",
                           key,
                           expectedValue
                       );
            }

            object actualValue;
            Subject.ActionArguments.TryGetValue(key, out actualValue);

            if (ReferenceEquals(expectedValue, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .ForCondition(actualValue == null)
                       .FailWith(
                           "Expected {context:actioncontext} to to have argument {0} with value <null>{reason}, but value was {1}.",
                           key,
                           actualValue
                       );
            }
            else {
                if (!ReferenceEquals(actualValue, null)) {
                    actualValue.Should().BeOfType(Nullable.GetUnderlyingType(expectedValueType) ?? expectedValueType);
                    actualValue.ShouldBeEquivalentTo(expectedValue, options => options.RespectingRuntimeTypes());
                }
            }

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> maps to a specified
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
        public AndConstraint<HttpActionContextAssertions> MapTo<THttpController>(string because = "",
                                                                                 params object[] reasonArgs)
            where THttpController : IHttpController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to not be <null>{reason}.");
            }

            var expectedController = HttpRouteDataFactory.HttpControllerName<THttpController>();
            Subject.Should().MapToController(expectedController, because, reasonArgs);

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> maps to a specified
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
        public AndConstraint<HttpActionContextAssertions> MapToController(string expectedController,
                                                                          string because = "",
                                                                          params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to not be <null>{reason}.");
            }

            Subject.ControllerContext.Controller.GetType().Name.Should().Be(expectedController, because, reasonArgs);

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> maps to a specified
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
        public AndConstraint<HttpActionContextAssertions> MapToAction(string expectedAction,
                                                                      string because = "",
                                                                      params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to not be <null>{reason}.");
            }

            Subject.ActionDescriptor.ActionName.Should().Be(expectedAction, because, reasonArgs);

            return new AndConstraint<HttpActionContextAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="HttpActionContext">actionContext</see> maps to a specified action.
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
        public AndConstraint<HttpActionContextAssertions> MapTo<THttpController>(
            Expression<Func<THttpController, object>> action,
            string because = "",
            params object[] reasonArgs)
            where THttpController : IHttpController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:actioncontext} to not be <null>{reason}.");
            }

            Subject.Should().MapTo<THttpController>(because, reasonArgs);
            Subject.Should().MapToAction(action.ActionName(), because, reasonArgs);

            //check parameters
            var methodArguments = action.GetMethodArgumentValues();
            foreach (var methodArgument in methodArguments) {
                var param = methodArgument.Key;
                var expectedValue = methodArgument.Value;
                var paramName = param.Name;

                if (param.ParameterType.CanBeNull()) {
                    Subject.Should().HaveOptionalActionArgument(paramName, expectedValue, param.ParameterType);
                }
                else {
                    Subject.Should().HaveActionArgument(paramName, expectedValue, param.ParameterType);
                }
            }

            return new AndConstraint<HttpActionContextAssertions>(this);
        }
    }
}
