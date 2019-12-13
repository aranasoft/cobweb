using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Cobweb.Testing.Mvc.Fakes {
    public class FakeHttpSessionState : HttpSessionStateBase {
        private SessionStateItemCollection _objects;

        public FakeHttpSessionState() {
            _objects = new SessionStateItemCollection();
        }

        public override HttpSessionStateBase Contents {
            get { return this; }
        }

        public override int Count {
            get { return _objects.Count; }
        }

        public override object this[string name] {
            get { return _objects[name]; }
            set { _objects[name] = value; }
        }

        public override object this[int index] {
            get { return _objects[index]; }
            set { _objects[index] = value; }
        }

        public override NameObjectCollectionBase.KeysCollection Keys {
            get { return _objects.Keys; }
        }

        public override void Abandon() {
            _objects = new SessionStateItemCollection();
        }

        public override void Add(string name, object value) {
            _objects[name] = value;
        }

        public override void Clear() {
            _objects.Clear();
        }

        public override void CopyTo(Array array, int index) {
            var entities = new object[_objects.Count];
            for (var entityIndex = 0; entityIndex < _objects.Count; entityIndex++) {
                entities[entityIndex] = _objects[entityIndex];
            }

            entities.CopyTo(array, index);
        }

        public override IEnumerator GetEnumerator() {
            return _objects.GetEnumerator();
        }

        public override void Remove(string name) {
            _objects.Remove(name);
        }

        public override void RemoveAll() {
            _objects.Clear();
        }

        public override void RemoveAt(int index) {
            _objects.RemoveAt(index);
        }
    }
}
