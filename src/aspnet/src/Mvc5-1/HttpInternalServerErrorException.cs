using System.Net;
using System.Web;

namespace Cobweb.Web.Mvc {
    public class HttpInternalServerErrorException : HttpException {
        public HttpInternalServerErrorException(string reason) :
            base((int) HttpStatusCode.InternalServerError, reason) {}
    }
}
