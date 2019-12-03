using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Cobweb.Web.Http.Filters {
    public class ValidationActionFilter : ActionFilterAttribute {
        public override void OnActionExecuting(HttpActionContext actionContext) {
            ModelStateDictionary modelState = actionContext.ModelState;

            if (!modelState.IsValid) {
                var clientModelState = new ModelStateDictionary();
                foreach (string key in modelState.Keys.Where(key => !string.IsNullOrWhiteSpace(key))) {
                    string revisedKey = key.IndexOf('.') > -1 ? key.Substring(key.IndexOf('.') + 1) : key;
                    foreach (ModelError error in modelState[key].Errors) {
                        clientModelState.AddModelError(revisedKey, error.ErrorMessage);
                    }
                }

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                                   clientModelState);
            }
        }
    }
}
