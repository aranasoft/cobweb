using System;
using System.Diagnostics;
using System.Web.Mvc;
using Cobweb.Extentions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that a <see cref="ViewResultBase" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class ViewResultAssertions : ReferenceTypeAssertions<ViewResultBase, ViewResultAssertions> {
        public ViewResultAssertions(ViewResultBase value) {
            Subject = value;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context {
            get { return "ViewResult"; }
        }

        /// <summary>
        ///     Asserts that an object does not equal another object using its <see cref="object.Equals(object)" /> method.
        /// </summary>
        /// <param name="because">
        ///     A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        ///     start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more values to use for filling in any <see cref="string.Format(string,object[])" /> compatible
        ///     placeholders.
        /// </param>
        public AndConstraint<ViewResultAssertions> HaveModelOfType<TViewModel>(string because = "",
                                                                               params object[] reasonArgs) {
            if (ReferenceEquals(Subject, null)) {
                Execute.Assertion
                       .BecauseOf(because, reasonArgs)
                       .FailWith(
                           "Expected {context:viewresult} to have model of type {0}{reason}, but {context:viewresult} was <null>.",
                           typeof(TViewModel).Name,
                           Subject);
            }

            var actualViewModel = Subject.ViewData.Model;

            Execute.Assertion
                   .ForCondition(actualViewModel.GetType().IsAssignableTo<TViewModel>())
                   .FailWith("Expected {context:viewresult} to have Model of type '{0}', but was '{1}'",
                             typeof(TViewModel).Name,
                             actualViewModel.GetType().Name);

            return new AndWhichConstraint<ViewResultAssertions, TViewModel>(this, (TViewModel) actualViewModel);
        }
    }
}
