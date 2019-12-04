using System.Linq;
using Aranasoft.Cobweb.NHibernate.Caching;

namespace Aranasoft.Cobweb.NHibernate.Tests.Util
{
#pragma warning disable 618
    public class FakeCachingProvider : FakeQueryableOptionsProvider, ICachingProvider {
#pragma warning restore 618
        public IQueryable<T> Cacheable<T>(IQueryable<T> query) {
            return query;
        }
    }
}
