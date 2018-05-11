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

        public static IFetchRequest<T> Fetch<T, TProperty>(
            this IQueryable<T> source,
            Expression<Func<T, TProperty>> path) {
            return GetCurrentProvider().Fetch(source, path);
        }

        public static IFetchRequest<T> FetchMany<T, TProperty>(
            this IQueryable<T> source,
            Expression<Func<T, IEnumerable<TProperty>>> path) {
            return GetCurrentProvider().FetchMany(source, path);
        }

        public static IFetchRequest<T> ThenFetch<T, TFetchedProperty, TChildProperty>(
            this IFetchRequest<T> source,
            Expression<Func<TFetchedProperty, TChildProperty>> path) {
            return GetCurrentProvider().ThenFetch(source, path);
        }

        public static IFetchRequest<T> ThenFetchMany<T, TFetchedProperty, TChildProperty>(
            this IFetchRequest<T> source,
            Expression<Func<TFetchedProperty, IEnumerable<TChildProperty>>> path) {
            return GetCurrentProvider().ThenFetchMany(source, path);
        }
    }
}
