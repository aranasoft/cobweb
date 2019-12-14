using System.Web.Mvc;

namespace Aranasoft.Cobweb.Mvc.Validation.Fakes {
    public class FakeViewDataContainer : IViewDataContainer {
        public ViewDataDictionary ViewData { get; set; }
    }
}
