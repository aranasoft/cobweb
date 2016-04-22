using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Cobweb.Testing.WebApi.Extensions
{
    public static class WithKeyValuePair {
        /// <summary>
        ///     Convert a KeyValuePair into a NameObjectCollection object.
        /// </summary>
        /// <param name="pairs">The KeyValuePair instances to convert.</param>
        /// <returns>A NameObjectCollection containing the keys and values from <paramref name="pairs" />.</returns>
        public static HttpRouteValueDictionary ToHttpRouteValueDictionary(this IEnumerable<KeyValuePair<string, object>> pairs) {
            return new HttpRouteValueDictionary(pairs);
        }
    }
}
