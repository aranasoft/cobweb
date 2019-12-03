using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Tests.TestableTypes {
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
