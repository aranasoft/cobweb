using System;
using System.Web.Mvc;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes {
    public class AlternateNamingController : Controller {
        public ActionResult Index() {
            return null;
        }

        [ActionName("ActionName")]
        public ActionResult WithAlternateActionName() {
            return null;
        }

        public ActionResult WithAlternateParameterName(string someParameter) {
            return null;
        }
    }
}
