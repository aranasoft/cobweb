using System;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Routing;
using Cobweb.Extentions.ObjectExtentions;
using Cobweb.Reflection.Extensions;
using Cobweb.Testing.WebApi.Fakes;

namespace Cobweb.Testing.WebApi {
    public static class RouteDataFactory {
        /// <summary>
        ///     Returns the corresponding named route.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <param name="httpMethod">The HTTP method</param>
        /// <returns>RouteData for the named route; null if no matching route was found.</returns>
        public static IHttpRouteData AsNamedRoute(this string name, string httpMethod) {
            var method = new HttpMethod(httpMethod);
            return AsNamedRoute(name, method);
        }

        /// <summary>
        ///     Returns the corresponding named route.  Returns null if no route was found.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <returns>RouteData for the named route; null if no matching route was found.</returns>
        public static IHttpRouteData AsNamedRoute(this string name, HttpMethod httpMethod = null) {
            var context = GetHttpRequestMessage(name, httpMethod);
            return GlobalConfiguration.Configuration.Routes[name].IfExists(route => route.GetRouteData(GlobalConfiguration.Configuration.VirtualPathRoot, context));
        }

        /// <summary>
        ///     Find the route for a URL and an Http Method
        ///     because you have a method contraint on the route
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static IHttpRouteData AsHttpRoute(this string url, string httpMethod) {
            var method = new HttpMethod(httpMethod);
            return AsHttpRoute(url, method);
        }

        /// <summary>
        ///     Returns the corresponding route for the URL.  Returns null if no route was found.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <returns></returns>
        public static IHttpRouteData AsHttpRoute(this string url, HttpMethod httpMethod = null)
        {
            var context = GetHttpRequestMessage(url, httpMethod);
            return AsHttpRoute(context);
        }
        public static IHttpRouteData AsHttpRoute(this HttpRequestMessage requestMessage)
        {
            return GlobalConfiguration.Configuration.Routes.GetRouteData(requestMessage);
        }

        /// <summary>
        ///     A way to start the fluent interface and and which method to use
        ///     since you have a method constraint in the route.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static HttpRequestMessage WithHttpMethod(this string url, string httpMethod) {
            var method = new HttpMethod(httpMethod);
            return AsRequest(url, method);
        }

        public static HttpRequestMessage WithHttpMethod(this string url, HttpMethod httpMethod) {
            return AsRequest(url, httpMethod);
        }

        public static HttpRequestMessage AsRequest(this string url, string httpMethod)
        {
            var method = new HttpMethod(httpMethod);
            return AsRequest(url, method);
        }

         public static HttpRequestMessage AsRequest(this string url, HttpMethod httpMethod = null)
        {
            return GetHttpRequestMessage(url, httpMethod);
        }


        private static HttpRequestMessage GetHttpRequestMessage(string url, HttpMethod httpMethod = null) {
            if (httpMethod == null) {
                httpMethod = HttpMethod.Get;
            }

            var uri = new Uri("http://localhost/" + url.Substring(2));
            var queryString = HttpUtility.ParseQueryString(uri.Query);

            var fakeRequest = new FakeHttpRequest
            {
                AppRelativeCurrentExecutionFilePath = "~" + uri.AbsolutePath,
                Form = new NameValueCollection(),
                HttpVerb = httpMethod,
                QueryString = queryString,
            };

            var fakeRequestMessage =  new HttpRequestMessage(httpMethod, uri);

            var fakeHttpContext = new FakeHttpContext { Request = fakeRequest };
            var requestContext = new RequestContext
            {
                HttpContext = fakeHttpContext,
                RouteData = new RouteData()
            };

            fakeRequestMessage.Properties[PropertyKeys.HttpContextKey] = fakeHttpContext;
            fakeRequestMessage.Properties[PropertyKeys.RequestContextKey] = requestContext;
            fakeRequestMessage.Properties[HttpPropertyKeys.HttpRouteDataKey] = fakeRequestMessage.AsHttpRoute();

            return fakeRequestMessage;
        }
    }
}
