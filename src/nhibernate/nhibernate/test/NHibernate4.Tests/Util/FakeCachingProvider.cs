using System.Linq;
using Aranasoft.Cobweb.NHibernate.Caching;

namespace Aranasoft.Cobweb.NHibernate.Tests.Util {
    public class FakeCachingProvider : ICachingProvider {
        public ICacheRequest<T> Cacheable<T>(
            IQueryable<T> query) {
            return new CacheRequest<T>(query);
        }
    }
}
