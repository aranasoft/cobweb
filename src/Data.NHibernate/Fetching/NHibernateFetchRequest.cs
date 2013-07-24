using NHibernate.Linq;

namespace Aranasoft.Cobweb.Data.NHibernate.Fetching {
    public class NHibernateFetchRequest<T, TProperty> :
        FetchRequest<INhFetchRequest<T, TProperty>, T> {
        public NHibernateFetchRequest(INhFetchRequest<T, TProperty> source) : base(source) {}
    }
}