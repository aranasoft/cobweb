using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cobweb.Testing.Mvc.Assertions {
    /// <summary>
    ///     Contains extension methods for custom assertions in unit tests.
    /// </summary>
    [DebuggerNonUserCode]
    public static class AssertionExtensions {
        /// <summary>
        ///     Returns a <see cref="RouteValueDictionaryAssertions" /> object that can be used to assert the current
        ///     <see cref="RouteValueDictionary" />.
        /// </summary>
        public static RouteValueDictionaryAssertions Should(this RouteValueDictionary actualValue) {
            return new RouteValueDictionaryAssertions(actualValue);
        }

        /// <summary>
        ///     Returns a <see cref="RouteDataAssertions" /> object that can be used to assert the current
        ///     <see cref="RouteData" />.
        /// </summary>
        public static RouteDataAssertions Should(this RouteData actualValue) {
            return new RouteDataAssertions(actualValue);
        }

        /// <summary>
        ///     Returns a <see cref="ViewResultAssertions" /> object that can be used to assert the current
        ///     <see cref="ViewResultBase" />.
        /// </summary>
        public static ViewResultAssertions Should(this ViewResultBase actualValue) {
            return new ViewResultAssertions(actualValue);
        }

        /// <summary>
        ///     Returns an <see cref="ActionResultAssertions" /> object that can be used to assert the current
        ///     <see cref="ActionResult" />.
        /// </summary>
        public static ActionResultAssertions Should(this ActionResult actualValue) {
            return new ActionResultAssertions(actualValue);
        }

        /// <summary>
        ///     Returns a <see cref="RedirectResultAssertions" /> object that can be used to assert the current
        ///     <see cref="RedirectResult" />.
        /// </summary>
        public static RedirectResultAssertions Should(this RedirectResult actualValue) {
            return new RedirectResultAssertions(actualValue);
        }

        /// <summary>
        ///     Returns a <see cref="RouteContextAssertions" /> object that can be used to assert the current
        ///     <see cref="IRouteContext" />.
        /// </summary>
        public static RouteContextAssertions Should(this IRouteContext actualValue) {
            return new RouteContextAssertions(actualValue);
        }

        /// <summary>
        ///     Returns a <see cref="ControllerActionExpressionAssertions{TController}" /> object that can be used to assert the current
        ///     ControllerActionExpression.
        /// </summary>
        public static ControllerActionExpressionAssertions<TController> Should<TController>(
            this Expression<Func<TController, ActionResult>> actualValue) where TController : Controller {
            return new ControllerActionExpressionAssertions<TController>(actualValue);
        }
    }
}
