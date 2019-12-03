using System;
using System.Linq;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.QueryableOptions {
    public class NHibernateQueryableOptionsProvider : IQueryableOptionsProvider {
        public IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions) {
            return source.WithOptions(setOptions);
        }
    }
}
