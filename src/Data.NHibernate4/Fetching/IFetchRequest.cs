using System.Linq;

namespace Cobweb.Data.NHibernate.Fetching {
    public interface IFetchRequest<out T> : IOrderedQueryable<T> {}
}
