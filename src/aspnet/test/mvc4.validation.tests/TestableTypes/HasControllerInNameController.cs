using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Tests.TestableTypes {
    public class HasControllerInNameController : Controller {
        public ActionResult Index() {
            return null;
        }

        public ActionResult Other() {
            return null;
        }
    }
}
