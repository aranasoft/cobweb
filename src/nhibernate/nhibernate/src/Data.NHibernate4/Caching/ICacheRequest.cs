using System.Linq;

namespace Cobweb.Data.NHibernate.Caching {
    public interface ICacheRequest<out T> : IQueryable<T> {}
}
