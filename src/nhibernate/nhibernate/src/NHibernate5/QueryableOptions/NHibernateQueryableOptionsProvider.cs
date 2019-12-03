using System;
using System.Linq;
using NHibernate.Linq;

namespace Aranasoft.Cobweb.NHibernate.QueryableOptions {
    public class NHibernateQueryableOptionsProvider : IQueryableOptionsProvider {
        public IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions) {
            return source.WithOptions(setOptions);
        }
    }
}
