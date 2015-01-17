using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cobweb.Data.NHibernate.Fetching {
    public interface IFetchingProvider {
        IFetchRequest<T> Fetch<T, TProperty>(
            IQueryable<T> source,
            Expression<Func<T, TProperty>> path);

        IFetchRequest<T> FetchMany<T, TProperty>(
            IQueryable<T> source,
            Expression<Func<T, IEnumerable<TProperty>>> path);

        IFetchRequest<T> ThenFetch<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> query,
            Expression<Func<TFetchedProperty, TChildProperty>> relatedObjectSelector);

        IFetchRequest<T> ThenFetchMany<T, TFetchedProperty, TChildProperty>(
            IFetchRequest<T> source,
            Expression<Func<TFetchedProperty, IEnumerable<TChildProperty>>> path);
    }
}
