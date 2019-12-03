using System;
using System.Diagnostics;
using System.Web.Mvc;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="RedirectResult" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class RedirectResultAssertions : ReferenceTypeAssertions<RedirectResult, RedirectResultAssertions> {
        public RedirectResultAssertions(RedirectResult value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "RedirectResult"; }
        }

        /// <summary>
        ///     Asserts that a <see cref="RedirectResult">redirectResult</see> maps to a specified url.
        /// </summary>
        /// <param name="expectedUrl">The expected url</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<RedirectResultAssertions> RedirectToUrl(string expectedUrl,
                                                                     string because = "",
                                                                     params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:redirectresult} to redirect to {0}, but {context:redirectresult} was <null>.",
                           expectedUrl);
            }

            Execute.Assertion
                   .ForCondition(string.Compare(Subject.Url, expectedUrl, StringComparison.InvariantCulture) == 0)
                   .FailWith("Expected {context:redirectresult} to redirect to {0}, but was {1}",
                             expectedUrl,
                             Subject.Url);

            return new AndConstraint<RedirectResultAssertions>(this);
        }
    }
}
