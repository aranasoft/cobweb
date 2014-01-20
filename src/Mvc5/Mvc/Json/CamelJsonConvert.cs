using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cobweb.Web.Mvc.Json {
    public class CamelJsonConvert {
        public static MvcHtmlString SerializeObject(object o) {
            var settings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            return new MvcHtmlString(JsonConvert.SerializeObject(o, Formatting.None, settings));
        }
    }
}
