using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aranasoft.Cobweb.Data.NHibernate.Fetching {
    public class FetchRequest<TRequest, TQueried> : IFetchRequest<TQueried>
        where TRequest : IQueryable<TQueried> {
        public IEnumerator<TQueried> GetEnumerator() {
            return Queryable.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
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

        public TRequest Queryable { get; private set; }

        public FetchRequest(TRequest source) {
            Queryable = source;
        }
    }
}