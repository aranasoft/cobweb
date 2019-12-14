using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aranasoft.Cobweb.Http.Formatting;

namespace Aranasoft.Cobweb.Http {
    public class HttpUnauthorizedResponseException : HttpResponseException {
        public HttpUnauthorizedResponseException(string reason, string errorMessage)
            : base(
                new HttpResponseMessage(HttpStatusCode.Unauthorized) {
                    ReasonPhrase = reason,
                    Content = new JsonErrorObjectContent(errorMessage)
                }) {}
    }
}
