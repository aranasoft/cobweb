using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cobweb.Data.NHibernate.Fetching {
    public class FetchRequest<TRequest, TQueried> : IFetchRequest<TQueried>
        where TRequest : IQueryable<TQueried> {
        public FetchRequest(TRequest source) {
            Queryable = source;
        }

        public TRequest Queryable { get; private set; }

        public IEnumerator<TQueried> GetEnumerator() {
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
