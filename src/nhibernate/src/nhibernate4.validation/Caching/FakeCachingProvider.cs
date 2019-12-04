using System.Linq;
using Aranasoft.Cobweb.NHibernate.Caching;

namespace Aranasoft.Cobweb.NHibernate.Validation.Caching {
    public class FakeCachingProvider : ICachingProvider {
        public ICacheRequest<T> Cacheable<T>(
            IQueryable<T> query) {
            return new CacheRequest<T>(query);
        }
    }
}
