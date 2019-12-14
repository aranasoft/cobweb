using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Routing;
using FluentAssertions;

namespace Aranasoft.Cobweb.Http.Validation.Assertions {
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="string" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public static class HttpRequestMessageAssertionExtentions {
        /// <summary>
        ///     Asserts that a <see cref="HttpRequestMessage">requestmessage</see> as <see cref="IHttpRouteData">routeData</see> is an ignored route.
        /// </summary>
        /// <param name="assertions">The assertion context.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public static AndConstraint<HttpRequestMessageAssertions> BeIgnoredRoute(
            this HttpRequestMessageAssertions assertions,
            string because = "",
            params object[] reasonArgs) {
            assertions.Subject.AsHttpRoute().Should().BeIgnored(because, reasonArgs);

            return new AndConstraint<HttpRequestMessageAssertions>(assertions);
        }
    }
}
