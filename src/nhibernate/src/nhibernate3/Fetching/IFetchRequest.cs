using System.Linq;

namespace Aranasoft.Cobweb.NHibernate.Fetching {
    public interface IFetchRequest<out TOriginatingEntity, TFetch> : IOrderedQueryable<TOriginatingEntity> {}
}
