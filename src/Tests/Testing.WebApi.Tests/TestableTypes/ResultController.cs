using System.Web.Http;

namespace Cobweb.Testing.WebApi.Tests.TestableTypes {
    public class ResultController : ApiController {
        public AnObject ReturnObject() {
            return new AnObject();
        }
    }
}
