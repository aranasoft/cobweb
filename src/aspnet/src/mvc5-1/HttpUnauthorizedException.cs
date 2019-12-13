using System.Net;
using System.Web;

namespace Cobweb.Web.Mvc {
    public class HttpUnauthorizedException : HttpException {
        public HttpUnauthorizedException(string reason) : base((int) HttpStatusCode.Unauthorized, reason) {}
    }
}
