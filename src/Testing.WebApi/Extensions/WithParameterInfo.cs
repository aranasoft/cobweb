using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Cobweb.Testing.WebApi.Extensions {
    public static class WithParameterInfo {
        public static bool IsBoundFromUri(this ParameterInfo param) {
            return param.GetCustomAttributes(typeof(FromUriAttribute), true).Any() ||
                   (!param.GetCustomAttributes(typeof(FromBodyAttribute), true).Any() &&
                    param.ParameterType.DefaultsToBoundFromUri());
        }

        public static bool IsBoundFromBody(this ParameterInfo param) {
            return param.GetCustomAttributes(typeof(FromBodyAttribute), true).Any() ||
                   (!param.GetCustomAttributes(typeof(FromUriAttribute), true).Any() &&
                    param.ParameterType.DefaultsToBoundFromBody());
        }
    }
}
