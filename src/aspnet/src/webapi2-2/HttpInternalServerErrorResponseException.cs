using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Formatting;

namespace Aranasoft.Cobweb.Http {
    public class HttpInternalServerErrorResponseException : HttpResponseException {
        public HttpInternalServerErrorResponseException(string reason, string errorMessage)
            : base(
                new HttpResponseMessage(HttpStatusCode.InternalServerError) {
                    ReasonPhrase = reason,
                    Content = new JsonErrorObjectContent(errorMessage)
                }) {}
    }
}
