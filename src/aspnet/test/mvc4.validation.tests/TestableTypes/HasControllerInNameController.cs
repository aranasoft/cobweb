using System.Web.Mvc;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes {
    public class HasControllerInNameController : Controller {
        public ActionResult Index() {
            return null;
        }

        public ActionResult Other() {
            return null;
        }
    }
}
