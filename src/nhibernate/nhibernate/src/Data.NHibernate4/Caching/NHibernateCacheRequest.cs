using System.Linq;

namespace Cobweb.Data.NHibernate.Caching {
    public class NHibernateCacheRequest<T> :
        CacheRequest<T> {
        public NHibernateCacheRequest(IQueryable<T> source) : base(source) {}
    }
}
