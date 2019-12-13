using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cobweb.Testing.Mvc.Fakes;

namespace Cobweb.Testing.Mvc {
    public static class ControllerFactory {
        public static T InitializeController<T>(this T controller,
                                                HttpContextBase httpContext,
                                                RouteData routeData,
                                                TempDataDictionary tempData) where T : Controller {
            routeData = routeData ?? new RouteData();
            httpContext = httpContext ?? new FakeHttpContext();
            tempData = tempData ?? new TempDataDictionary();

            var controllerContext = new ControllerContext(httpContext, routeData, controller);

            controller.ControllerContext = controllerContext;
            controller.TempData = tempData;
            controller.Url = new UrlHelper(controllerContext.RequestContext);

            return controller;
        }

        public static T CreateController<T>(params object[] constructorArgs) where T : Controller {
            return (T) Activator.CreateInstance(typeof(T), constructorArgs);
        }

        public static T ResolveController<T>() where T : Controller {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}
