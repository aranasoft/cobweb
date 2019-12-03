using System.Web.Http;

namespace Cobweb.Testing.WebApi.Tests.TestableTypes {
    public class ActionController : ApiController {
        [HttpGet]
        public void OtherGet() {}

        [HttpGet]
        public void OtherGet(int id) {}

        [HttpPost]
        public void OtherPost() {}

        [HttpPost]
        public void OtherPost(int id) {}

        [HttpPut]
        public void OtherPut() {}

        [HttpPut]
        public void OtherPut(int id) {}

        [HttpDelete]
        public void OtherDelete() {}

        [HttpDelete]
        public void OtherDelete(int id) {}
    }
}
