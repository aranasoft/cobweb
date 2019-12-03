using System.Linq;

namespace Cobweb.Data.NHibernate.Caching {
    public interface ICachingProvider {
        ICacheRequest<T> Cacheable<T>(IQueryable<T> source);
    }
}
