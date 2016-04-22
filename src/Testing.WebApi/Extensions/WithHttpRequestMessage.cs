using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Cobweb.Extentions.ObjectExtentions;

namespace Cobweb.Testing.WebApi.Extensions {
    public static class WithHttpRequestMessage {
        public static HttpControllerDescriptor GetControllerDescriptor(this HttpRequestMessage subject) {
            GlobalConfiguration.Configuration.Services.Replace(typeof (IAssembliesResolver),
                                                               new DefaultAssembliesResolver());
            var controllerSelector = GlobalConfiguration.Configuration.Services.GetHttpControllerSelector();
            return controllerSelector.IfExists(selector => selector.SelectController(subject));
        }

        public static HttpActionDescriptor GetActionDescriptor(this HttpRequestMessage subject)
        {
            var controllerDescriptor = GetControllerDescriptor(subject);
            var routeData = GlobalConfiguration.Configuration.Routes.GetRouteData(subject);
            var controllerContext = new HttpControllerContext(GlobalConfiguration.Configuration, routeData, subject)
            {
                ControllerDescriptor = controllerDescriptor
            };
            var actionSelector = new ApiControllerActionSelector();
            var actionDescriptor = actionSelector.SelectAction(controllerContext);
            return actionDescriptor;
        }
    }
}
