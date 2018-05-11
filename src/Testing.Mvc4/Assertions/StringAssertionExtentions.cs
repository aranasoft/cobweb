using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Testing.Mvc.Extensions;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="string" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public static class StringAssertionExtentions {
        /// <summary>
        ///     Asserts that a string as <see cref="RouteData">routeData</see> is an ignored route.
        /// </summary>
        /// <param name="assertions">The assertion context.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public static AndConstraint<StringAssertions> BeIgnoredRoute(this StringAssertions assertions,
                                                                     string because = "",
                                                                     params object[] reasonArgs) {
            assertions.Subject.AsRoute().Should().BeIgnored(because, reasonArgs);

            return new AndConstraint<StringAssertions>(assertions);
        }

        /// <summary>
        ///     Asserts that a string as <see cref="RouteData" /> maps to a specified WebForms page.
        /// </summary>
        /// <param name="assertions">The assertion context.</param>
        /// <param name="expectedVirtualPath">The ~/ based path to the WebForms page</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public static AndConstraint<StringAssertions> MapToPage(this StringAssertions assertions,
                                                                string expectedVirtualPath,
                                                                string because = "",
                                                                params object[] reasonArgs) {
            assertions.Subject.AsRoute().Should().MapToPage(expectedVirtualPath, because, reasonArgs);

            return new AndConstraint<StringAssertions>(assertions);
        }

        /// <summary>
        ///     Asserts that a string as <see cref="RouteData" /> maps to a specified controller.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="assertions">The assertion context.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public static AndConstraint<StringAssertions> MapTo<TController>(this StringAssertions assertions,
                                                                         string because = "",
                                                                         params object[] reasonArgs)
            where TController : Controller {
            var routeData = assertions.Subject.AsRoute();
            routeData.Should().MapTo<TController>(because, reasonArgs);

            return new AndConstraint<StringAssertions>(assertions);
        }

        /// <summary>
        ///     Asserts that a string as <see cref="RouteData" /> maps to a specified action.
        /// </summary>
        /// <typeparam name="TController">The type of controller.</typeparam>
        /// <param name="assertions">The assertion context.</param>
        /// <param name="action">The action to call on <typeparamref name="TController" />.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        /// <remarks>Assertions are peformed against the specified controller, action, and action parameters.</remarks>
        public static AndConstraint<StringAssertions> MapTo<TController>(this StringAssertions assertions,
                                                                         Expression<Func<TController, ActionResult>>
                                                                             action,
                                                                         string because = "",
                                                                         params object[] reasonArgs)
            where TController : Controller {
            assertions.Subject.AsRoute().Should().MapTo(action, because, reasonArgs);

            return new AndConstraint<StringAssertions>(assertions);
        }
    }
}
