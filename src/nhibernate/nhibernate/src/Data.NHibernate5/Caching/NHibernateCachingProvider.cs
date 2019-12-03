using System;
using System.Linq;
using Cobweb.Data.NHibernate.QueryableOptions;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Caching {
    [Obsolete("Please use NHibernateQueryableOptionsProvider instead.")]
    public class NHibernateCachingProvider : NHibernateQueryableOptionsProvider, ICachingProvider {
        public IQueryable<T> Cacheable<T>(
            IQueryable<T> source) {
            return source.WithOptions(o => o.SetCacheable(true));
        }
    }
}
