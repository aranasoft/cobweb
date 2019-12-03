using System;
using System.Collections;
using System.Web;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeHttpFileCollection : FakeHttpFileCollectionBase {
        private readonly NameObjectCollection<HttpPostedFileBase> _files;

        public FakeHttpFileCollection() {
            _files = new NameObjectCollection<HttpPostedFileBase>();
        }


        public override string[] AllKeys {
            get { return _files.AllKeys; }
        }

        public override int Count {
            get { return _files.Count; }
        }

        public new virtual HttpPostedFileBase this[string name] {
            get { return _files[name]; }
            set { _files[name] = value; }
        }

        public new virtual HttpPostedFileBase this[int index] {
            get { return _files[index]; }
            set { _files[index] = value; }
        }

        public override KeysCollection Keys {
            get { return _files.Keys; }
        }

        public override void CopyTo(Array array, int index) {
            _files.CopyTo(array, index);
        }

        public override HttpPostedFileBase Get(int index) {
            return _files.Get(index);
        }

        public override HttpPostedFileBase Get(string name) {
            return _files.Get(name);
        }

        public override string GetKey(int index) {
            return _files.GetKey(index);
        }

        public override IEnumerator GetEnumerator() {
            return _files.GetEnumerator();
        }
    }
}
