using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cobweb.Data.NHibernate.Fetching;

namespace Cobweb.Data.NHibernate.Providers {
    public static class EagerFetch {
        private static Func<IFetchingProvider> _currentProvider = () => new NHibernateFetchingProvider();

        public static Func<IFetchingProvider> Current {
            get { return _currentProvider; }
            set { _currentProvider = value; }
        }

        private static IFetchingProvider GetCurrentProvider() {
            const string message = @"Unable to perform eager fetch. No Eager Fetch provider has been specified.";
            if (_currentProvider == null) {
                throw new InvalidOperationException(message);
            }

            IFetchingProvider fetchingProvider = _currentProvider();
            if (fetchingProvider == null) {
                throw new InvalidOperationException(message);
            }

            return fetchingProvider;
        }

        public static IFetchRequest<TOriginatingEntity, TFetch> Fetch<TOriginatingEntity, TFetch>(
            this IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, TFetch>> path) {
            return GetCurrentProvider().Fetch(source, path);
        }

        public static IFetchRequest<TOriginatingEntity, TFetch> FetchMany<TOriginatingEntity, TFetch>(
            this IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, IEnumerable<TFetch>>> path) {
            return GetCurrentProvider().FetchMany(source, path);
        }

        public static IFetchRequest<TOriginatingEntity, TNestedFetch> ThenFetch<
            TOriginatingEntity, TFetchOn, TNestedFetch>(
            this IFetchRequest<TOriginatingEntity, TFetchOn> source,
            Expression<Func<TFetchOn, TNestedFetch>> path) {
            return GetCurrentProvider().ThenFetch(source, path);
        }

        public static IFetchRequest<TOriginatingEntity, TNestedFetch> ThenFetchMany<
            TOriginatingEntity, TFetchOn, TNestedFetch>(
            this IFetchRequest<TOriginatingEntity, TFetchOn> source,
            Expression<Func<TFetchOn, IEnumerable<TNestedFetch>>> path) {
            return GetCurrentProvider().ThenFetchMany(source, path);
        }
    }
}
