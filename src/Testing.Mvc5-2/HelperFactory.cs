using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Testing.Mvc.Fakes;
using Moq;

namespace Cobweb.Testing.Mvc {
    public static class HelperFactory {
        public static HtmlHelper HtmlHelper(ViewDataDictionary viewData = null, HttpRequestBase request = null) {
            viewData = viewData ?? new ViewDataDictionary();
            request = request ?? new FakeHttpRequest();
            var httpContext = new FakeHttpContext {Request = request};

            var controllerContext = new ControllerContext {
                HttpContext = httpContext,
                RouteData = new RouteData()
            };

            var viewContext = new ViewContext(
                controllerContext,
                Mock.Of<IView>(),
                viewData,
                new TempDataDictionary(),
                new StringWriter());

            var viewDataContainer = new FakeViewDataContainer {ViewData = viewData};

            return new HtmlHelper(viewContext, viewDataContainer);
        }

        public static UrlHelper UrlHelper(HttpRequestBase request = null) {
            request = request ?? new FakeHttpRequest();
            var httpContext = new FakeHttpContext {Request = request};
            var requestContext = new RequestContext {
                HttpContext = httpContext,
                RouteData = new RouteData()
            };

            return new UrlHelper(requestContext);
        }
    }
}
