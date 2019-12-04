using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.QueryableOptions;

namespace Aranasoft.Cobweb.NHibernate.Providers {
    [Obsolete("Please use QueryableOptionsProvider instead.")]
    public static class CachingProvider {
        [Obsolete("Please use QueryableOptionsProvider.Current instead.")]
        public static Func<IQueryableOptionsProvider> Current {
            get { return QueryableOptionsProvider.Current; }
            set { QueryableOptionsProvider.Current = value; }
        }

        [Obsolete("Please use QueryableOptionsProvider.WithOptions instead.")]
        public static IQueryable<T> Cacheable<T>(IQueryable<T> source) {
            return source.Cacheable();
        }
    }
}
