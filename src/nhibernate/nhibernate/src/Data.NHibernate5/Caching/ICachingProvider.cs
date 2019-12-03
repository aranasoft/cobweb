using System;
using System.Linq;
using Cobweb.Data.NHibernate.QueryableOptions;

namespace Cobweb.Data.NHibernate.Caching {
    [Obsolete("Please use IQueryableOptionsProvider instead.")]
    public interface ICachingProvider : IQueryableOptionsProvider {
        [Obsolete("Please use IQueryableOptions.WithOptions instead.")]
        IQueryable<T> Cacheable<T>(IQueryable<T> source);
    }
}
