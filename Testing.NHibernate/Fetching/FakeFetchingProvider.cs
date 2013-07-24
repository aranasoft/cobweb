using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Aranasoft.Cobweb.Data.NHibernate.Fetching;

namespace Aranasoft.Cobweb.Testing.NHibernate.Fetching {
    public class FakeFetchingProvider : IFetchingProvider {
        public IFetchRequest<T> Fetch<T, TProperty>(
            IQueryable<T> query,
            Expression<Func<T, TProperty>> relatedObjectSelector) {
            return new FetchRequest<IQueryable<T>, T>(query);
        }

        public IFetchRequest<T> FetchMany<T, TProperty>(
            IQueryable<T> query,
            Expression<Func<T, IEnumerable<TProperty>>> relatedObjectSelector) {
            return new FetchRequest<IQueryable<T>, T>(query);
        }

        public IFetchRequest<T> ThenFetch<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> query,
            Expression<Func<TFetchedProperty, TChildProperty>> relatedObjectSelector) {
            var fetchRequest = (FetchRequest<IQueryable<T>, T>) query;
            return new FetchRequest<IQueryable<T>, T>(fetchRequest.Queryable);
        }

        public IFetchRequest<T> ThenFetchMany<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> query,
            Expression<Func<TFetchedProperty, IEnumerable<TChildProperty>>> relatedObjectSelector) {
            var fetchRequest = (FetchRequest<IQueryable<T>, T>) query;
            return new FetchRequest<IQueryable<T>, T>(fetchRequest.Queryable);
        }
    }
}