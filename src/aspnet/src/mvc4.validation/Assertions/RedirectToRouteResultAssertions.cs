using System;
using System.Diagnostics;
using System.Web.Mvc;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Testing.Mvc.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="RedirectToRouteResult" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class RedirectToRouteResultAssertions :
        ReferenceTypeAssertions<RedirectToRouteResult, RedirectToRouteResultAssertions> {
        public RedirectToRouteResultAssertions(RedirectToRouteResult value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "RedirectToRouteResult"; }
        }

        /// <summary>
        ///     Asserts that a <see cref="RedirectToRouteResult">redirectResult</see> redirects to a specified
        ///     <typeparamref name="TController">controller</typeparamref>.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RedirectToRouteResultAssertions> RedirectToController<TController>(string because = "",
                                                                                                params object[]
                                                                                                    reasonArgs)
            where TController : IController {
            const string key = "Controller";
            var expectedController = RouteDataFactory.ControllerName<TController>();
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:redirecttorouteresult} to redirect to controller {0}{reason}, but {context:redirecttorouteresult} was <null>.",
                           expectedController);
            }


            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.RouteValues.ContainsKey(key))
                   .FailWith(
                       "Expected {context:redirecttorouteresult} to to have route parameter {0}{reason}, but parameter {0} was not found.",
                       key
                   );

            var actualController = Subject.RouteValues.ContainsKey(key) &&
                                   !string.IsNullOrEmpty(Subject.RouteValues[key].IfExists(val => val.ToString()))
                ? Subject.RouteValues[key].IfExists(val => val.ToString())
                : null;

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(
                       string.Compare(expectedController,
                                      actualController,
                                      StringComparison.InvariantCultureIgnoreCase) ==
                       0)
                   .FailWith(
                       "Expected {context:redirecttorouteresult} to redirect to controller {0}{reason}, but was {1}.",
                       expectedController,
                       actualController
                   );

            return new AndConstraint<RedirectToRouteResultAssertions>(this);
        }
    }
}
