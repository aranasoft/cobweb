using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Tests.TestableTypes {
    public class ViewResultController : Controller {
        public ActionResult RenderViewWithViewName() {
            return View("FirstView");
        }


        public ActionResult RenderViewWithViewNameAndData() {
            return View("SecondView", new {Prop1 = 1, Prop2 = 2});
        }


        public ActionResult RenderViewWithViewNameAndMaster() {
            return View("ThirdView", "ThirdMaster");
        }


        public ActionResult RenderViewWithViewNameAndMasterAndData() {
            return View("FourthView", "FourthMaster", new {Prop1 = 3, Prop2 = 4});
        }
    }
}
