using System.Linq;

namespace Cobweb.Data.NHibernate.Fetching {
    public interface IFetchRequest<out TOriginatingEntity, TFetch> : IOrderedQueryable<TOriginatingEntity> {}
}
