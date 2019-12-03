using System.Web.Mvc;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeViewDataContainer : IViewDataContainer {
        public ViewDataDictionary ViewData { get; set; }
    }
}
