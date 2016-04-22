using System;
using System.Web.Http;

namespace Cobweb.Testing.WebApi.Tests.TestableTypes {
    public class ActionDataTypeInputController : ApiController {
        public void Index() {}

        [HttpGet]
        public void WithString(string id) {}

        [HttpGet]
        public void WithNothing() {}

        [HttpPost]
        public void PostNothing() {}

        [HttpGet]
        public void WithObject(AnObject id) {}

        [HttpGet]
        public void WithInteger(int id) {}

        [HttpPost]
        public void PostInteger(int id) {}

        [HttpGet]
        public void WithNullInteger(int? id) {}

        [HttpGet]
        public void WithGuid(Guid id) {}

        [HttpGet]
        public void WithDateTime(DateTime id) {}

        [HttpGet]
        public void WithNullDateTime(DateTime? id) {}
    }
}
