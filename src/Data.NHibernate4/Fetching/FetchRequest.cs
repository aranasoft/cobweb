using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cobweb.Data.NHibernate.Fetching {
    public class FetchRequest<TCollection, TOriginatingEntity, TFetch> : IFetchRequest<TOriginatingEntity, TFetch>
        where TCollection : IQueryable<TOriginatingEntity> {
        public FetchRequest(TCollection source) {
            Queryable = source;
        }

        public TCollection Queryable { get; private set; }

        public IEnumerator<TOriginatingEntity> GetEnumerator() {
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
