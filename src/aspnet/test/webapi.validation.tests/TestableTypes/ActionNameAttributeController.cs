using System;
using System.Web.Http;

namespace Aranasoft.Cobweb.Http.Validation.Tests.TestableTypes {
    public class ActionNameAttributeController : ApiController {
        public void Index() {}

        [HttpGet]
        [ActionName("GetActionName")]
        public void GetWithAlternateActionName() {}

        [HttpGet]
        [ActionName("GetActionName")]
        public void GetWithAlternateActionName(int id) {}

        [HttpPost]
        [ActionName("PostActionName")]
        public void PostWithAlternateActionName() {}

        [HttpPost]
        [ActionName("PostActionName")]
        public void PostWithAlternateActionName(int id) {}

        [HttpPut]
        [ActionName("PutActionName")]
        public void PutWithAlternateActionName() {}

        [HttpPut]
        [ActionName("PutActionName")]
        public void PutWithAlternateActionName(int id) {}

        [HttpDelete]
        [ActionName("DeleteActionName")]
        public void DeleteWithAlternateActionName() {}

        [HttpDelete]
        [ActionName("DeleteActionName")]
        public void DeleteWithAlternateActionName(int id) {}
    }
}
