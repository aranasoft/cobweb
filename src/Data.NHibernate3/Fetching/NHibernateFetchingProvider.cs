using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Fetching {
    public class NHibernateFetchingProvider : IFetchingProvider {
        public IFetchRequest<T> Fetch<T, TProperty>(
            IQueryable<T> source,
            Expression<Func<T, TProperty>> path) {
            var fetchQuery = source.Fetch(path);
            return new NHibernateFetchRequest<T, TProperty>(fetchQuery);
        }

        public IFetchRequest<T> FetchMany<T, TProperty>(
            IQueryable<T> source,
            Expression<Func<T, IEnumerable<TProperty>>> path) {
            var fetchQuery = source.FetchMany(path);
            return new NHibernateFetchRequest<T, TProperty>(fetchQuery);
        }

        public IFetchRequest<T> ThenFetch<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> query,
            Expression<Func<TFetchedProperty, TChildProperty>> relatedObjectSelector) {
            var fetchRequest = (NHibernateFetchRequest<T, TFetchedProperty>) query;
            var fetchQuery = fetchRequest.Queryable.ThenFetch(relatedObjectSelector);
            return new NHibernateFetchRequest<T, TChildProperty>(fetchQuery);
        }

        public IFetchRequest<T> ThenFetchMany<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> source,
            Expression<Func<TFetchedProperty, IEnumerable<TChildProperty>>> path) {
            var fetchRequest = (NHibernateFetchRequest<T, TFetchedProperty>) source;
            var fetchQuery = fetchRequest.Queryable.ThenFetchMany(path);
            return new NHibernateFetchRequest<T, TChildProperty>(fetchQuery);
        }
    }
}
