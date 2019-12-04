using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.QueryableOptions;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    [Obsolete("Please use IQueryableOptionsProvider instead.")]
    public interface ICachingProvider : IQueryableOptionsProvider {
        [Obsolete("Please use IQueryableOptions.WithOptions instead.")]
        IQueryable<T> Cacheable<T>(IQueryable<T> source);
    }
}
