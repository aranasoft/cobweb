using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.Caching;
using Aranasoft.Cobweb.NHibernate.Validation.QueryableOptions;

namespace Aranasoft.Cobweb.NHibernate.Validation.Caching {
    [Obsolete("Please use FakeQueryableOptionsProvider instead.")]
    public class FakeCachingProvider : FakeQueryableOptionsProvider, ICachingProvider {
        public IQueryable<T> Cacheable<T>(IQueryable<T> query) {
            return query;
        }
    }
}
