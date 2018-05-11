using System;
using System.Diagnostics;
using System.Web.Mvc;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="ActionResult" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class ActionResultAssertions : ReferenceTypeAssertions<ActionResult, ActionResultAssertions> {
        public ActionResultAssertions(ActionResult value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "ActionResult"; }
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="ViewResultBase" />.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<ActionResultAssertions, ViewResultBase> ReturnView(string because = "",
                                                                                     params object[] reasonArgs) {
            return BeOfType<ViewResultBase>(because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="ViewResultBase" /> with a specified View
        ///     Name.
        /// </summary>
        /// <param name="expectedViewName">The expected View name</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<ActionResultAssertions, ViewResultBase> ReturnView(string expectedViewName,
                                                                                     string because = "",
                                                                                     params object[] reasonArgs) {
            var viewResult = ReturnView(because, reasonArgs).Which;
            Execute.Assertion
                   .ForCondition(
                       string.Compare(expectedViewName,
                                      viewResult.ViewName,
                                      StringComparison.InvariantCulture) ==
                       0)
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected ViewResult to have ViewName {0}{reason}, but was {1}.",
                             expectedViewName,
                             viewResult.ViewName);

            return new AndWhichConstraint<ActionResultAssertions, ViewResultBase>(this, viewResult);
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="JsonResult" />.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<ActionResultAssertions, JsonResult> ReturnJson(string because = "",
                                                                                 params object[] reasonArgs) {
            return BeOfType<JsonResult>(because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="RedirectResult" /> or
        ///     <see cref="RedirectToRouteResult" />.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ActionResultAssertions> ReturnRedirect(string because = "", params object[] reasonArgs) {
            Execute.Assertion
                   .ForCondition(!ReferenceEquals(Subject, null))
                   .BecauseOf(because, reasonArgs)
                   .FailWith("Expected type to be RedirectResult or RedirectToRouteResult{reason}, but found <null>.");

            Execute.Assertion
                   .BecauseOf(because, reasonArgs)
                   .ForCondition(Subject.IsSameOrEqualTo(typeof(RedirectResult)) ||
                                 Subject.IsSameOrEqualTo(typeof(RedirectToRouteResult)))
                   .FailWith(
                       "Expected {context:object} to be RedirectResult or RedirectToRouteResult{reason}, but found {0}.",
                       Subject);

            return new AndConstraint<ActionResultAssertions>(this);
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="RedirectResult" />.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<ActionResultAssertions, RedirectResult> ReturnUrlRedirect(string because = "",
                                                                                            params object[]
                                                                                                reasonArgs) {
            return BeOfType<RedirectResult>(because, reasonArgs);
        }

        /// <summary>
        ///     Asserts that an <see cref="ActionResult">actionResult</see> is a <see cref="RedirectToRouteResult" />.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<ActionResultAssertions, RedirectToRouteResult> ReturnRouteRedirect(
            string because = "",
            params object[] reasonArgs) {
            return BeOfType<RedirectToRouteResult>(because, reasonArgs);
        }
    }
}
