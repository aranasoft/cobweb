using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;

namespace Cobweb.Testing.WebApi.Extensions {
    public static class WithHttpRouteValueDictionary {
        private const string SubRouteDataKey = "MS_SubRoutes";

        public static IEnumerable<IHttpRouteData> GetSubRoutes(this HttpRouteValueDictionary routeValues) {
            object subRoutes = null;
            if (routeValues.TryGetValue(SubRouteDataKey, out subRoutes)) {
                return subRoutes as IHttpRouteData[];
            }

            return null;
        }

        public static bool ContainsRouteValue(this HttpRouteValueDictionary routeValues, string key) {
            if (routeValues.ContainsKey(key)) {
                return true;
            }

            return routeValues.GetSubRoutes()?.FirstOrDefault()?.Values.ContainsKey(key) ?? false;
        }

        public static bool TryGetRouteValue(this HttpRouteValueDictionary routeValues, string key, out object value) {
            if (routeValues.TryGetValue(key, out value)) {
                return true;
            }

            return routeValues.GetSubRoutes()?.FirstOrDefault()?.Values.TryGetValue(key, out value) ?? false;
        }
    }
}
