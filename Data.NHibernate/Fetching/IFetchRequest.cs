using System.Linq;

namespace Aranasoft.Cobweb.Data.NHibernate.Fetching {
    public interface IFetchRequest<out T> : IOrderedQueryable<T> {}
}