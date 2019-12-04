using System;
using System.Linq;
using Cobweb.Data.NHibernate.Caching;
using Cobweb.Testing.NHibernate.QueryableOptions;

namespace Cobweb.Testing.NHibernate.Caching {
    [Obsolete("Please use FakeQueryableOptionsProvider instead.")]
    public class FakeCachingProvider : FakeQueryableOptionsProvider, ICachingProvider {
        public IQueryable<T> Cacheable<T>(IQueryable<T> query) {
            return query;
        }
    }
}
