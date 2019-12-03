using System;
using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Tests.TestableTypes {
    public class DataTypeInputController : Controller {
        public ActionResult Index() {
            return null;
        }

        public ActionResult WithString(string id) {
            return null;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult WithNothing() {
            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostNothing() {
            return null;
        }

        public ActionResult WithObject(AnObject id) {
            return null;
        }

        public ActionResult WithInteger(int id) {
            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostInteger(int id) {
            return null;
        }

        public ActionResult WithNullInteger(int? id) {
            return null;
        }

        public ActionResult WithGuid(Guid id) {
            return null;
        }

        public ActionResult WithDateTime(DateTime id) {
            return null;
        }

        public ActionResult WithNullDateTime(DateTime? id) {
            return null;
        }
    }
}
