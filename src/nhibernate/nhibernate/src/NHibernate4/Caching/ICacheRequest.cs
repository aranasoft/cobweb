using System.Linq;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    public interface ICacheRequest<out T> : IQueryable<T> {}
}
