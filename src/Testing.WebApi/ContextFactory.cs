using System.Net.Http;
using System.Web.Routing;
using Cobweb.Testing.WebApi.Fakes;

namespace Cobweb.Testing.WebApi {
    public static class ContextFactory {
   

        public static HttpRequestMessage HttpRequestMessage(HttpRequestMessage requestMessage = null) {
            requestMessage = requestMessage ?? new HttpRequestMessage();

            var httpContext = new FakeHttpContext {Request = new FakeHttpRequest()};
            var requestContext = new RequestContext {
                HttpContext = httpContext,
                RouteData = new RouteData()
            };

            requestMessage.Properties[PropertyKeys.HttpContextKey] = httpContext;
            requestMessage.Properties[PropertyKeys.RequestContextKey] = requestContext;

            return requestMessage;
        }
    }
}
