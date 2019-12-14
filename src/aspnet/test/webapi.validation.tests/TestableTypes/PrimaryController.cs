using System.Web.Http;

namespace Aranasoft.Cobweb.Http.Validation.Tests.TestableTypes {
    public class PrimaryController : ApiController {
        public void Get() {}
        public void Get(int id) {}
        public void Post() {}
        public void Post(int id) {}
        public void Put() {}
        public void Put(int id) {}
        public void Delete() {}
        public void Delete(int id) {}
    }
}
