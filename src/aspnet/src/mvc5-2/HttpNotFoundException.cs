using System.Net;
using System.Web;

namespace Aranasoft.Cobweb.Mvc {
    public class HttpNotFoundException : HttpException {
        public HttpNotFoundException(string reason) : base((int) HttpStatusCode.NotFound, reason) {}
    }
}
