using System;
using System.Linq;
using Cobweb.Data.NHibernate.QueryableOptions;
using NHibernate;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Providers {
    public static class QueryableOptionsProvider {
        private static Func<IQueryableOptionsProvider> _currentProvider = () => new NHibernateQueryableOptionsProvider();

        public static Func<IQueryableOptionsProvider> Current {
            get { return _currentProvider; }
            set { _currentProvider = value; }
        }

        private static IQueryableOptionsProvider GetCurrentProvider() {
            const string message = @"Unable to perform QueryOptions. No cache provider has been specified.";
            if (_currentProvider == null) {
                throw new InvalidOperationException(message);
            }

            IQueryableOptionsProvider queryableOptionsProvider = _currentProvider();
            if (queryableOptionsProvider == null) {
                throw new InvalidOperationException(message);
            }

            return queryableOptionsProvider;
        }

        [Obsolete("Please use WithOptions instead.")]
        public static IQueryable<T> Cacheable<T>(this IQueryable<T> query) {
            return GetCurrentProvider().WithOptions(query, options => options.SetCacheable(true));
        }

        [Obsolete("Please use WithOptions instead.")]
        public static IQueryable<T> CacheMode<T>(this IQueryable<T> query, CacheMode cacheMode) {
            return GetCurrentProvider().WithOptions(query, options => options.SetCacheMode(cacheMode));
        }

        [Obsolete("Please use WithOptions instead.")]
        public static IQueryable<T> CacheRegion<T>(this IQueryable<T> query, string region) {
            return GetCurrentProvider().WithOptions(query, options => options.SetCacheRegion(region));
        }

        [Obsolete("Please use WithOptions instead.")]
        public static IQueryable<T> Timeout<T>(this IQueryable<T> query, int timeout) {
            return GetCurrentProvider().WithOptions(query, options => options.SetTimeout(timeout));
        }

        [Obsolete("Please use WithOptions instead.")]
        public static IQueryable<T> SetOptions<T>(this IQueryable<T> source, Action<IQueryableOptions> setOptions) {
            return GetCurrentProvider().WithOptions(source, setOptions);
        }

        public static IQueryable<T> WithOptions<T>(this IQueryable<T> source, Action<NhQueryableOptions> withOptions) {
            return GetCurrentProvider().WithOptions(source, withOptions);
        }
    }
}
