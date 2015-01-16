using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cobweb.Web.Mvc {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AnonymousOnlyAttribute : ActionMethodSelectorAttribute {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo) {
            return IsAnonymous(controllerContext.HttpContext);
        }

        public virtual bool IsAnonymous(HttpContextBase httpContext) {
            return httpContext.Request.IsAuthenticated == false;
        }
    }
}
