using System.Net;
using System.Web;

namespace Cobweb.Web.Mvc {
    public class HttpBadRequestException : HttpException {
        public HttpBadRequestException(string reason) : base((int) HttpStatusCode.BadRequest, reason) {}
    }
}
