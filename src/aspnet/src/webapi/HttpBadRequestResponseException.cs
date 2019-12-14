using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Formatting;

namespace Aranasoft.Cobweb.Http {
    public class HttpBadRequestResponseException : HttpResponseException {
        public HttpBadRequestResponseException(string reason, string errorMessage)
            : base(
                new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = reason,
                    Content = new JsonErrorObjectContent(errorMessage)
                }) {}
    }
}
