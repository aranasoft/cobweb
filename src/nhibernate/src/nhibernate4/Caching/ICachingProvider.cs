using System.Linq;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    public interface ICachingProvider {
        ICacheRequest<T> Cacheable<T>(IQueryable<T> source);
    }
}
