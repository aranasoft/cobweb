using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cobweb.Data.NHibernate.Fetching {
    public interface IFetchingProvider {
        IFetchRequest<TOriginatingEntity, TFetch> Fetch<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, TFetch>> path);

        IFetchRequest<TOriginatingEntity, TFetch> FetchMany<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, IEnumerable<TFetch>>> path);

        IFetchRequest<TOriginatingEntity, TNestedFetch> ThenFetch<TOriginatingEntity, TFetchOn, TNestedFetch>(
            IFetchRequest<TOriginatingEntity, TFetchOn> source,
            Expression<Func<TFetchOn, TNestedFetch>> path);

        IFetchRequest<TOriginatingEntity, TNestedFetch> ThenFetchMany<TOriginatingEntity, TFetchOn, TNestedFetch>(
            IFetchRequest<TOriginatingEntity, TFetchOn> source,
            Expression<Func<TFetchOn, IEnumerable<TNestedFetch>>> path);
    }
}
