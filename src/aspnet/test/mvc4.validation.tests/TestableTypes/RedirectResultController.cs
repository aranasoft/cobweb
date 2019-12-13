using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Tests.TestableTypes {
    public class RedirectResultController : Controller {
        public ActionResult RedirectWithAction() {
            return RedirectToAction("FirstAction");
        }

        public ActionResult RedirectWithActionAndController() {
            return RedirectToAction("SecondAction", "SecondController");
        }

        public ActionResult RedirectWithObject() {
            return RedirectToAction("ThirdAction", "ThirdController", new {Id = 1});
        }
    }
}
