using System.Web.Http;

namespace Cobweb.Testing.WebApi.Tests.TestableTypes {
    public class HttpMethodAttributeController : ApiController {
        [HttpGet]
        public void ExecuteMethodGet() {}

        [HttpGet]
        public void ExecuteMethodGet(int id) {}

        [HttpPost]
        public void ExecuteMethodPost() {}

        [HttpPost]
        public void ExecuteMethodPost(int id) {}

        [HttpPut]
        public void ExecuteMethodPut() {}

        [HttpPut]
        public void ExecuteMethodPut(int id) {}

        [HttpDelete]
        public void ExecuteMethodDelete() {}

        [HttpDelete]
        public void ExecuteMethodDelete(int id) {}
    }
}
