using System.Web.Http;

namespace Cobweb.Testing.WebApi.Tests.TestableTypes {
    [RoutePrefix("attrib")]
    public class RouteAttributeController : ApiController {
        [HttpGet]
        [Route("")]
        public void Get() {}

        [HttpGet]
        [Route("{id:int}")]
        public void Get(int id) {}

        [HttpPost]
        [Route("")]
        public void Post() {}

        [HttpPost]
        [Route("{id:int}")]
        public void Post(int id) {}

        [HttpPut]
        [Route("")]
        public void Put() {}

        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id) {}

        [HttpDelete]
        [Route("")]
        public void Delete() {}

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id) {}
    }
}
