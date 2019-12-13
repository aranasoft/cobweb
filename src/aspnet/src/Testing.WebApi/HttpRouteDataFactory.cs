using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Cobweb.Reflection.Extensions;

namespace Cobweb.Testing.WebApi {
    public static class HttpRouteDataFactory {
        public static string HttpControllerName<THttpController>() where THttpController : IHttpController {
            return HttpControllerName(typeof(THttpController));
        }

        public static string HttpControllerName(Type type) {
            var typeName = type.Name;
            const string suffix = "Controller";
            return typeName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? typeName.Substring(0, typeName.Length - suffix.Length)
                : typeName;
        }

        public static string HttpControllerName<THttpController>(this Expression<Func<THttpController, object>> action)
            where THttpController : IHttpController {
            return HttpControllerName<THttpController>();
        }

        public static string ActionName<THttpController>(this Expression<Func<THttpController, object>> action)
            where THttpController : IHttpController {
            var method = ((MethodCallExpression) action.Body).Method;

            var actionNameAttribute = method.GetCustomAttribute<ActionNameAttribute>();
            return actionNameAttribute != null ? actionNameAttribute.Name : method.Name;
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
        public static IHttpRouteData AsHttpRoute(this string url, HttpMethod httpMethod = null) {
            var context = GetHttpRequestMessage(url, httpMethod);
            return AsHttpRoute(context);
        }

        public static IHttpRouteData AsHttpRoute(this HttpRequestMessage requestMessage) {
            return requestMessage.GetRouteData();
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

        public static HttpRequestMessage AsRequest(this string url, string httpMethod) {
            var method = new HttpMethod(httpMethod);
            return AsRequest(url, method);
        }

        public static HttpRequestMessage AsRequest(this string url, HttpMethod httpMethod = null) {
            return GetHttpRequestMessage(url, httpMethod);
        }

        public static HttpRequestMessage WithContent(this string url, string content) {
            return url.AsRequest().WithContent(content);
        }

        public static HttpRequestMessage WithContent(this HttpRequestMessage requestMessage, string content) {
            var payload = new StringContent(content);
            requestMessage.Content = payload;
            return requestMessage;
        }

        public static HttpRequestMessage WithJsonContent(this HttpRequestMessage requestMessage, string jsonContent) {
            var payload = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            requestMessage.Content = payload;
            return requestMessage;
        }

        private static HttpRequestMessage GetHttpRequestMessage(string url, HttpMethod httpMethod = null) {
            if (httpMethod == null) {
                httpMethod = HttpMethod.Get;
            }

            var uri = new Uri("http://localhost/" + url.Substring(2));
            var fakeRequestMessage = new HttpRequestMessage(httpMethod, uri);

            return fakeRequestMessage;
        }

        public static HttpRequestMessage UsingConfiguration(this HttpRequestMessage requestMessage,
                                                            HttpConfiguration configuration) {
            requestMessage.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
            var routeData = configuration.Routes.GetRouteData(requestMessage);
            if (routeData != null) {
                requestMessage.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            }

            return requestMessage;
        }

        public static HttpRequestMessage UsingConfiguration(this string url, HttpConfiguration configuration) {
            return url.AsRequest().UsingConfiguration(configuration);
        }
    }
}
