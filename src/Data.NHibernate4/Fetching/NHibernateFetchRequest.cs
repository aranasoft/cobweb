using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Fetching {
    public class NHibernateFetchRequest<TOriginatingEntity, TFetch> :
        FetchRequest<INhFetchRequest<TOriginatingEntity, TFetch>, TOriginatingEntity, TFetch> {
        public NHibernateFetchRequest(INhFetchRequest<TOriginatingEntity, TFetch> source) : base(source) {}
    }
}
