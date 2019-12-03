using System;
using Cobweb.Extentions;
using Cobweb.Reflection.Extensions;
using Cobweb.Testing.Mvc.Extensions;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cobweb.Testing.Mvc {
    public static class OutboundUrl {
        public static IRouteContext NamedRouteContext(string routeName) {
            return new NamedRouteContext(routeName);
        }

        public static IRouteContext RouteContext<TController>(this Expression<Func<TController, ActionResult>> action)
            where TController : Controller {
            var controllerName = RouteDataFactory.ControllerName<TController>();
            var actionName = action.ActionName();

            var routeValues = new RouteValueDictionary();
            action.GetMethodArgumentValues()
                  .ForEach(arg =>
                               routeValues.Add(arg.Key.Name, arg.Value)
                  );

            return new DerivedRouteContext(controllerName, actionName, routeValues);
        }
    }

    public class DerivedRouteContext : IRouteContext {
        private readonly string _action;
        private readonly string _controller;
        private readonly RouteValueDictionary _routeValues;

        internal DerivedRouteContext(string controller, string action, RouteValueDictionary routeValues) {
            _controller = controller;
            _action = action;
            _routeValues = routeValues;
        }

        public string GetUrl(UrlHelper helper = null) {
            helper = helper ?? HelperFactory.UrlHelper();
            return helper.Action(_action, _controller, _routeValues);
        }
    }

    public class NamedRouteContext : IRouteContext {
        private readonly string _routeName;

        internal NamedRouteContext(string routeName) {
            _routeName = routeName;
        }

        public string GetUrl(UrlHelper helper = null) {
            helper = helper ?? HelperFactory.UrlHelper();
            return helper.RouteUrl(_routeName);
        }
    }

    public interface IRouteContext {
        string GetUrl(UrlHelper helper);
    }
}
