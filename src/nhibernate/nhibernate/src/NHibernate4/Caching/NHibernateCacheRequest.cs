using System.Linq;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    public class NHibernateCacheRequest<T> :
        CacheRequest<T> {
        public NHibernateCacheRequest(IQueryable<T> source) : base(source) {}
    }
}
