using System.Linq;
using Cobweb.Data.NHibernate.Caching;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class FakeCachingProvider : ICachingProvider {
        public ICacheRequest<T> Cacheable<T>(
            IQueryable<T> query) {
            return new CacheRequest<T>(query);
        }
    }
}
