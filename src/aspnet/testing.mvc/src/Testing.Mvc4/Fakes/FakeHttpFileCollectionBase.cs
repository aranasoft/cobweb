using System;
using System.Linq;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public abstract class FakeHttpFileCollectionBase : HttpFileCollectionBase {
        public override sealed HttpPostedFileBase this[string name] {
            get { return Get(name); }
        }

        public override sealed HttpPostedFileBase this[int index] {
            get { return Get(index); }
        }

        public override abstract HttpPostedFileBase Get(string name);
        public override abstract HttpPostedFileBase Get(int index);
    }
}
