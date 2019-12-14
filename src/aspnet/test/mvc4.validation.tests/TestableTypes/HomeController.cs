using System.Web.Mvc;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.TestableTypes {
    public class HomeController : Controller {
        public ActionResult Index() {
            return null;
        }

        public ActionResult Other() {
            return null;
        }

        public ActionResult Item(int id) {
            return null;
        }
    }
}
