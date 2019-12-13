using System;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Reflection.Extensions;
using Cobweb.Testing.Mvc.Fakes;

namespace Cobweb.Testing.Mvc.Extensions {
    public static class RouteDataFactory {
        public static string ControllerName<TController>() where TController : IController {
            return ControllerName(typeof(TController));
        }

        public static string ControllerName(Type type) {
            var typeName = type.Name;
            const string suffix = "Controller";
            return typeName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? typeName.Substring(0, typeName.Length - suffix.Length)
                : typeName;
        }

        public static string ControllerName<TController>(this Expression<Func<TController, ActionResult>> action)
            where TController : IController {
            return ControllerName<TController>();
        }

        public static string ActionName<TController>(this Expression<Func<TController, ActionResult>> action)
            where TController : IController {
            var method = ((MethodCallExpression) action.Body).Method;

            var actionNameAttribute = method.GetCustomAttribute<ActionNameAttribute>();
            return actionNameAttribute != null ? actionNameAttribute.Name : method.Name;
        }

        /// <summary>
        ///     Returns the corresponding named route.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <param name="httpMethod">The HTTP method</param>
        /// <returns>RouteData for the named route; null if no matching route was found.</returns>
        public static RouteData AsNamedRoute(this string name, string httpMethod) {
            var verb = (HttpVerbs) Enum.Parse(typeof(HttpVerbs), httpMethod);
            return AsNamedRoute(name, verb);
        }

        /// <summary>
        ///     Returns the corresponding named route.  Returns null if no route was found.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <returns>RouteData for the named route; null if no matching route was found.</returns>
        public static RouteData AsNamedRoute(this string name, HttpVerbs httpMethod = HttpVerbs.Get) {
            var context = GetHttpContext(name, httpMethod);
            return RouteTable.Routes[name].IfExists(route => route.GetRouteData(context));
        }

        /// <summary>
        ///     Find the route for a URL and an Http Method
        ///     because you have a method contraint on the route
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static RouteData AsRoute(this string url, string httpMethod) {
            var verb = (HttpVerbs) Enum.Parse(typeof(HttpVerbs), httpMethod);
            return AsRoute(url, verb);
        }

        /// <summary>
        ///     Returns the corresponding route for the URL.  Returns null if no route was found.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <returns></returns>
        public static RouteData AsRoute(this string url, HttpVerbs httpMethod = HttpVerbs.Get) {
            var context = GetHttpContext(url, httpMethod);
            return RouteTable.Routes.GetRouteData(context);
        }

        /// <summary>
        ///     Returns the corresponding route for the URL.  Returns null if no route was found.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="formMethod">The form method.</param>
        /// <returns></returns>
        public static RouteData AsRoute(this string url, HttpVerbs httpMethod, HttpVerbs formMethod) {
            var context = GetHttpContext(url, httpMethod, formMethod);
            return RouteTable.Routes.GetRouteData(context);
        }

        /// <summary>
        ///     A way to start the fluent interface and and which method to use
        ///     since you have a method constraint in the route.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static RouteData WithMethod(this string url, string httpMethod) {
            return AsRoute(url, httpMethod);
        }

        public static RouteData WithMethod(this string url, HttpVerbs verb) {
            return AsRoute(url, verb);
        }

        /// <summary>
        ///     Asserts that the route matches the expression specified based on the incoming HttpMethod and FormMethod for Simply
        ///     Restful routing.  Checks controller, action, and any method arguments
        ///     into the action as route values.
        /// </summary>
        /// <param name="url">The relative URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="formMethod">The form method.</param>
        /// <returns></returns>
        public static RouteData WithMethod(this string url, HttpVerbs httpMethod, HttpVerbs formMethod) {
            return url.AsRoute(httpMethod, formMethod);
        }

        private static HttpContextBase GetHttpContext(string url,
                                                      HttpVerbs httpMethod = HttpVerbs.Get,
                                                      HttpVerbs? formMethod = null) {
            var uri = new Uri("http://localhost/" + url.Substring(2));

            var queryString = HttpUtility.ParseQueryString(uri.Query);

            var form = new NameValueCollection();
            if (formMethod.HasValue) {
                form.Add("_method", formMethod.Value.ToString("g"));
            }

            var fakeRequest = new FakeHttpRequest {
                AppRelativeCurrentExecutionFilePath = "~" + uri.AbsolutePath,
                Form = form,
                HttpVerb = httpMethod,
                QueryString = queryString,
            };

            return new FakeHttpContext {Request = fakeRequest};
        }
    }
}
