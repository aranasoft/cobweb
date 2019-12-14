using System.Web.Http.Routing;

namespace Aranasoft.Cobweb.Http.Validation.Extensions {
    public static class WithHttpRouteValueDictionary {
        public static bool ContainsRouteValue(this HttpRouteValueDictionary routeValues, string key) {
            return routeValues.ContainsKey(key);
        }

        public static bool TryGetRouteValue(this HttpRouteValueDictionary routeValues, string key, out object value) {
            return routeValues.TryGetValue(key, out value);
        }
    }
}
