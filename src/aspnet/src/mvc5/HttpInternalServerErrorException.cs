using System.Net;
using System.Web;

namespace Aranasoft.Cobweb.Mvc {
    public class HttpInternalServerErrorException : HttpException {
        public HttpInternalServerErrorException(string reason) :
            base((int) HttpStatusCode.InternalServerError, reason) {}
    }
}
