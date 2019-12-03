using System;
using System.Linq;
using Cobweb.Data.NHibernate.Caching;

namespace Cobweb.Data.NHibernate.Providers {
    public static class CachingProvider {
        private static Func<ICachingProvider> _currentProvider = () => new NHibernateCachingProvider();

        public static Func<ICachingProvider> Current {
            get { return _currentProvider; }
            set { _currentProvider = value; }
        }

        private static ICachingProvider GetCurrentProvider() {
            const string message = @"Unable to perform caching. No cache provider has been specified.";
            if (_currentProvider == null) {
                throw new InvalidOperationException(message);
            }

            ICachingProvider cachingProvider = _currentProvider();
            if (cachingProvider == null) {
                throw new InvalidOperationException(message);
            }

            return cachingProvider;
        }

        public static ICacheRequest<T> Cacheable<T>(this IQueryable<T> source) {
            return GetCurrentProvider().Cacheable(source);
        }
    }
}
