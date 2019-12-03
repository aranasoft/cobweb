using System.Linq;
using Cobweb.Data.NHibernate.Caching;

namespace Cobweb.Data.NHibernate.Tests.Util
{
#pragma warning disable 618
    public class FakeCachingProvider : FakeQueryableOptionsProvider, ICachingProvider {
#pragma warning restore 618
        public IQueryable<T> Cacheable<T>(IQueryable<T> query) {
            return query;
        }
    }
}
