using System.Net;
using System.Web;

namespace Cobweb.Web.Mvc {
    public class HttpForbiddenException : HttpException {
        public HttpForbiddenException(string reason) : base((int) HttpStatusCode.Forbidden, reason) {}
    }
}
