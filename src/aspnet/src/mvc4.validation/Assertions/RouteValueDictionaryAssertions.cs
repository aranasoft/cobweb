using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Extentions;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Reflection.Extensions;
using Cobweb.Testing.Mvc.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="RouteValueDictionary" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class RouteValueDictionaryAssertions :
        ReferenceTypeAssertions<RouteValueDictionary, RouteValueDictionaryAssertions> {
        public RouteValueDictionaryAssertions(RouteValueDictionary value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => "RouteValueDictionary";

        /// <summary>
        /// Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> does not contain any items.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> BeEmpty(string because = "", params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to be empty{reason}, but found {0}.", Subject);
            }

            Execute.Assertion
                   .ForCondition(!Subject.Any())
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected {context:routevalues} to not have any items{reason}, but found {0}.",
                             Subject.Count);

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        /// Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> contains at least 1 item.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> NotBeEmpty(string because = "",
                                                                        params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} not to be empty{reason}, but found {0}.", Subject);
            }

            Execute.Assertion
                   .ForCondition(Subject.Any())
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected {context:routevalues} to have one or more items{reason}, but found none.");

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }


        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> has a specified route parameter.
        /// </summary>
        /// <param name="key">The route parameter key.</param>
        /// <param name="expectedValue">The expected route parameter value.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> HaveEquivalentParameter(string key,
                                                                                     string expectedValue,
                                                                                     string because = "",
                                                                                     params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:routevalues} to to have parameter {0}{reason}, but {context:routevalues} was <null>.",
                           key);
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.ContainsKey(key))
                   .FailWith(
                       "Expected {context:routevalues} to to have parameter {0}{reason}, but parameter {0} was not found in {context:routevalues}.",
                       key
                   );

            var actualValue = Subject.ContainsKey(key) &&
                              !string.IsNullOrEmpty(Subject[key].IfExists(val => val.ToString()))
                ? Subject[key].IfExists(val => val.ToString())
                : null;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(
                       string.Compare(expectedValue,
                                      actualValue,
                                      StringComparison.InvariantCultureIgnoreCase) ==
                       0)
                   .FailWith(
                       "Expected {context:routevalues} to to have parameter {0} with value {1}{reason}, but value was {2}.",
                       key,
                       expectedValue,
                       actualValue
                   );

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> has a specified route parameter.
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
        public AndConstraint<RouteValueDictionaryAssertions> HaveParameter<TValue>(string key,
                                                                                   TValue expectedValue,
                                                                                   string because = "",
                                                                                   params object[] reasonArgs)
            where TValue : class {
            return HaveParameter(key, expectedValue, typeof(TValue), because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> has a specified route parameter.
        /// </summary>
        /// <param name="key">The route parameter key.</param>
        /// <param name="expectedValue">The expected route parameter value.</param>
        /// <param name="expectedValueType">The type of <paramref name="expectedValue" /></param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> HaveParameter(string key,
                                                                           object expectedValue,
                                                                           Type expectedValueType,
                                                                           string because = "",
                                                                           params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:routevalues} to to have parameter {0}{reason}, but {context:routevalues} was <null>.",
                           key);
            }

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.ContainsKey(key))
                   .FailWith(
                       "Expected {context:routevalues} to to have parameter {0}{reason}, but parameter {0} was not found in {context:routevalues}.",
                       key
                   );

            var actualValue = Subject.ContainsKey(key) &&
                              !string.IsNullOrEmpty(Subject[key].IfExists(val => val.ToString()))
                ? Subject[key]
                : null;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(CompareParameterValues(expectedValueType, expectedValue, actualValue))
                   .FailWith(
                       "Expected {context:routevalues} to to have parameter {0} with value {1}{reason}, but value was {2}.",
                       key,
                       expectedValue,
                       actualValue
                   );

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> has an optional specified route
        ///     parameter.
        /// </summary>
        /// <typeparam name="TValue">The type of <paramref name="expectedValue" /></typeparam>
        /// <param name="key">The route parameter key.</param>
        /// <param name="expectedValue">The expected route parameter value/</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> HaveOptionalParameter<TValue>(string key,
                                                                                           TValue expectedValue,
                                                                                           string because = "",
                                                                                           params object[] reasonArgs)
            where TValue : class {
            return HaveOptionalParameter(key, expectedValue, typeof(TValue), because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> has an optional specified route
        ///     parameter.
        /// </summary>
        /// <param name="key">The route parameter key.</param>
        /// <param name="expectedValue">The expected route parameter value/</param>
        /// <param name="expectedValueType">The type of <paramref name="expectedValue" /></param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RouteValueDictionaryAssertions> HaveOptionalParameter(string key,
                                                                                   object expectedValue,
                                                                                   Type expectedValueType,
                                                                                   string because = "",
                                                                                   params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:routevalues} to to have optional parameter {0}{reason}, but {context:routevalues} was <null>.",
                           key);
            }

            if (!ReferenceEquals(expectedValue, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .ForCondition(Subject.ContainsKey(key))
                       .FailWith(
                           "Expected {context:routevalues} to to have parameter {0} with value {1}{reason}, but parameter {0} was not found in {context:routevalues}.",
                           key,
                           expectedValue
                       );
            }

            var actualValue = Subject.ContainsKey(key) &&
                              !string.IsNullOrEmpty(Subject[key].IfExists(val => val.ToString()))
                ? Subject[key]
                : null;


            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(CompareParameterValues(expectedValueType, expectedValue, actualValue))
                   .FailWith(
                       "Expected {context:routevalues} to to have parameter {0} with value {1}{reason}, but value was {2}.",
                       key,
                       expectedValue,
                       actualValue
                   );


            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        private bool CompareParameterValues(Type expectedValueType, object expectedValue, object actualValue) {
            actualValue = ConvertParameterValueToType(actualValue, expectedValueType);
            return (actualValue == null && expectedValue == null) ||
                   (expectedValue != null && expectedValue.Equals(actualValue));
        }

        private object ConvertParameterValueToType(object value, Type type) {
            if (ReferenceEquals(value, null)) {
                return null;
            }

            if (type.IsValueType) {
                if (Nullable.GetUnderlyingType(type) != null) {
                    return ConvertParameterValueToType(value, Nullable.GetUnderlyingType(type));
                }

                if (type == typeof(Guid)) {
                    Guid convertedValue;
                    return Guid.TryParse(value.ToString(), out convertedValue) ? convertedValue : value;
                }

                try {
                    return Convert.ChangeType(value, type);
                }
                catch (Exception) {
                    // ignored
                }
            }

            return value;
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> maps to a specified
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
        public AndConstraint<RouteValueDictionaryAssertions> MapTo<TController>(string because = "",
                                                                                params object[] reasonArgs)
            where TController : IController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to not be <null>{reason}.");
            }

            var expectedController = RouteDataFactory.ControllerName<TController>();
            Subject.Should().MapToController(expectedController, because, reasonArgs);

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> maps to a specified
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
        public AndConstraint<RouteValueDictionaryAssertions> MapToController(string expectedController,
                                                                             string because = "",
                                                                             params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to not be <null>{reason}.");
            }

            Subject.Should().HaveEquivalentParameter("controller", expectedController, because, reasonArgs);

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> maps to a specified
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
        public AndConstraint<RouteValueDictionaryAssertions> MapToAction(string expectedAction,
                                                                         string because = "",
                                                                         params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to not be <null>{reason}.");
            }

            Subject.Should().HaveEquivalentParameter("action", expectedAction, because, reasonArgs);

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }

        /// <summary>
        ///     Asserts that a <see cref="RouteValueDictionary">routeValueDictionary</see> maps to a specified action.
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
        public AndConstraint<RouteValueDictionaryAssertions> MapTo<TController>(
            Expression<Func<TController, ActionResult>> action,
            string because = "",
            params object[] reasonArgs)
            where TController : IController {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith("Expected {context:routevalues} to not be <null>{reason}.");
            }

            Subject.Should().MapTo<TController>(because, reasonArgs);
            Subject.Should().MapToAction(action.ActionName(), because, reasonArgs);

            //check parameters
            var methodArguments = action.GetMethodArgumentValues();
            foreach (var methodArgument in methodArguments) {
                var param = methodArgument.Key;
                var expectedValue = methodArgument.Value;
                var paramName = param.Name;

                if (param.ParameterType.CanBeNull()) {
                    Subject.Should().HaveOptionalParameter(paramName, expectedValue, param.ParameterType);
                }
                else {
                    Subject.Should().HaveParameter(paramName, expectedValue, param.ParameterType);
                }
            }

            return new AndConstraint<RouteValueDictionaryAssertions>(this);
        }
    }
}
