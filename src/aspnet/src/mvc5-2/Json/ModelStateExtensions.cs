using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cobweb.Web.Mvc.Json {
    public static class ModelStateExtensions {
        public static object[] ToSerializedObject(this ModelStateDictionary modelState) {
            return modelState.Where(e => e.Value.Errors.Any()).Select(GetModelErrorObjectForJson).ToArray();
        }

        private static object GetModelErrorObjectForJson(KeyValuePair<string, ModelState> error) {
            return
                new {
                    Name = error.Key,
                    Errors =
                        error.Value.Errors.Select(x => x.ErrorMessage)
                             .Concat(error.Value.Errors.Where(x => x.Exception != null)
                                          .Select(x => x.Exception.Message))
                             .ToArray()
                };
        }
    }
}
