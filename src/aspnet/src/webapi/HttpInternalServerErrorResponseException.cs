using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cobweb.Web.Http.Formatting;

namespace Cobweb.Web.Http {
    public class HttpInternalServerErrorResponseException : HttpResponseException {
        public HttpInternalServerErrorResponseException(string reason, string errorMessage)
            : base(
                new HttpResponseMessage(HttpStatusCode.InternalServerError) {
                    ReasonPhrase = reason,
                    Content = new JsonErrorObjectContent(errorMessage)
                }) {}
    }
}
