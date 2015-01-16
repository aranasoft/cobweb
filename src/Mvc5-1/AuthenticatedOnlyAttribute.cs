using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cobweb.Web.Mvc {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticatedOnlyAttribute : ActionMethodSelectorAttribute {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo) {
            return IsAuthenticated(controllerContext.HttpContext);
        }

        public virtual bool IsAuthenticated(HttpContextBase httpContextBase) {
            return httpContextBase.Request.IsAuthenticated;
        }
    }
}
