using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using Aranasoft.Cobweb.Reflection.Extensions;

namespace Aranasoft.Cobweb.Http.Validation.Extensions {
    public static class WithHttpController {
        public static string ControllerName<THttpController>() where THttpController : IHttpController {
            return ControllerName(typeof(THttpController));
        }

        public static string ControllerName(this Type type) {
            var typeName = type.Name;
            const string suffix = "Controller";
            return typeName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)
                ? typeName.Substring(0, typeName.Length - suffix.Length)
                : typeName;
        }

        public static string ControllerName<THttpController>(this Expression<Action<THttpController>> action)
            where THttpController : IHttpController {
            return ControllerName<THttpController>();
        }

        public static string ActionName<THttpController>(this Expression<Action<THttpController>> action)
            where THttpController : IHttpController {
            var method = ((MethodCallExpression) action.Body).Method;

            var actionNameAttribute = method.GetCustomAttribute<ActionNameAttribute>();
            return actionNameAttribute != null ? actionNameAttribute.Name : method.Name;
        }
    }
}
