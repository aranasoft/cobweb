using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Fetching {
    public class NHibernateFetchingProvider : IFetchingProvider {
        public IFetchRequest<TOriginatingEntity, TFetch> Fetch<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, TFetch>> path) {
            var fetchQuery = source.Fetch(path);
            return new NHibernateFetchRequest<TOriginatingEntity, TFetch>(fetchQuery);
        }

        public IFetchRequest<TOriginatingEntity, TFetch> FetchMany<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, IEnumerable<TFetch>>> path) {
            var fetchQuery = source.FetchMany(path);
            return new NHibernateFetchRequest<TOriginatingEntity, TFetch>(fetchQuery);
        }

        public IFetchRequest<TOriginalEntity, TNestedFetch> ThenFetch<TOriginalEntity, TFetchOn, TNestedFetch>(
            IFetchRequest<TOriginalEntity, TFetchOn> source,
            Expression<Func<TFetchOn, TNestedFetch>> path) {
            var fetchRequest = (NHibernateFetchRequest<TOriginalEntity, TFetchOn>) source;
            var fetchQuery = fetchRequest.Queryable.ThenFetch(path);
            return new NHibernateFetchRequest<TOriginalEntity, TNestedFetch>(fetchQuery);
        }

        public IFetchRequest<TOriginalEntity, TNestedFetch> ThenFetchMany<TOriginalEntity, TFetchOn, TNestedFetch>(
            IFetchRequest<TOriginalEntity, TFetchOn> source,
            Expression<Func<TFetchOn, IEnumerable<TNestedFetch>>> path) {
            var fetchRequest = (NHibernateFetchRequest<TOriginalEntity, TFetchOn>) source;
            var fetchQuery = fetchRequest.Queryable.ThenFetchMany(path);
            return new NHibernateFetchRequest<TOriginalEntity, TNestedFetch>(fetchQuery);
        }
    }
}
