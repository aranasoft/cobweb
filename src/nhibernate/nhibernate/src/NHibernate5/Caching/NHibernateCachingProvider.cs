using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.QueryableOptions;
using NHibernate.Linq;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    [Obsolete("Please use NHibernateQueryableOptionsProvider instead.")]
    public class NHibernateCachingProvider : NHibernateQueryableOptionsProvider, ICachingProvider {
        public IQueryable<T> Cacheable<T>(
            IQueryable<T> source) {
            return source.WithOptions(o => o.SetCacheable(true));
        }
    }
}
