using System;
using Cobweb.Extentions;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cobweb.Testing.Mvc {
    public class NameObjectCollection<T> : NameObjectCollectionBase {
        public virtual T this[string name] {
            get { return Get(name); }
            set { BaseSet(name, value); }
        }

        public virtual T this[int index] {
            get { return Get(index); }
            set { BaseSet(index, value); }
        }

        public virtual string[] AllKeys {
            get { return BaseGetAllKeys(); }
        }

        public virtual void Add(string name, T value) {
            BaseAdd(name, value);
        }

        public virtual void AddRange(IEnumerable<KeyValuePair<string, T>> values) {
            values.ForEach(pair => { BaseAdd(pair.Key, pair.Value); });
        }

        public virtual void CopyTo(Array array, int index) {
            BaseGetAllValues().CopyTo(array, index);
        }

        public virtual T Get(string name) {
            var item = BaseGet(name);
            return item is T ? (T) item : default(T);
        }

        public virtual T Get(int index) {
            var item = BaseGet(index);
            return item is T ? (T) item : default(T);
        }

        public virtual string GetKey(int index) {
            return BaseGetKey(index);
        }

        public virtual void Remove(string name) {
            BaseRemove(name);
        }

        public virtual void RemoveAll() {
            BaseClear();
        }

        public virtual void RemoveAt(int index) {
            BaseRemoveAt(index);
        }
    }
}
