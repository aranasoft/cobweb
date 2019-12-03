using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cobweb.Data.NHibernate.Caching {
    public class CacheRequest<T> : ICacheRequest<T> {
        public CacheRequest(IQueryable<T> source) {
            Queryable = source;
        }

        public IQueryable<T> Queryable { get; private set; }

        public IEnumerator<T> GetEnumerator() {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Queryable.GetEnumerator();
        }

        public Type ElementType {
            get { return Queryable.ElementType; }
        }

        public Expression Expression {
            get { return Queryable.Expression; }
        }

        public IQueryProvider Provider {
            get { return Queryable.Provider; }
        }
    }
}
