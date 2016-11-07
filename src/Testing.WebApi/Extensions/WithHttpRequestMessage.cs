using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Cobweb.Testing.WebApi.Extensions {
    public static class WithHttpRequestMessage {
        public static HttpControllerDescriptor SelectController(this HttpRequestMessage subject) {
            var httpConfiguration = subject.GetConfiguration();
            httpConfiguration.Services.Replace(typeof(IAssembliesResolver),
                                               new DefaultAssembliesResolver());
            var controllerSelector = httpConfiguration.Services.GetHttpControllerSelector();
            return controllerSelector.SelectController(subject);
        }

        public static HttpActionDescriptor SelectAction(this HttpRequestMessage subject) {
            var controllerDescriptor = SelectController(subject);
            var httpConfiguration = subject.GetConfiguration();
            var routeData = httpConfiguration.Routes.GetRouteData(subject);
            var controllerContext = new HttpControllerContext(httpConfiguration, routeData, subject) {
                ControllerDescriptor = controllerDescriptor
            };
            var actionSelector = new ApiControllerActionSelector();
            var actionDescriptor = actionSelector.SelectAction(controllerContext);
            return actionDescriptor;
        }
    }
}
