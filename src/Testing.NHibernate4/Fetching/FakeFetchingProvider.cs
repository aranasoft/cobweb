﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cobweb.Data.NHibernate.Fetching;

namespace Cobweb.Testing.NHibernate.Fetching {
    public class FakeFetchingProvider : IFetchingProvider {
        public IFetchRequest<TOriginatingEntity, TFetch> Fetch<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, TFetch>> path) {
            return new FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TFetch>(source);
        }

        public IFetchRequest<TOriginatingEntity, TFetch> FetchMany<TOriginatingEntity, TFetch>(
            IQueryable<TOriginatingEntity> source,
            Expression<Func<TOriginatingEntity, IEnumerable<TFetch>>> path) {
            return new FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TFetch>(source);
        }

        public IFetchRequest<TOriginatingEntity, TNestedFetch> ThenFetch<TOriginatingEntity, TFetchOn, TNestedFetch>(
            IFetchRequest<TOriginatingEntity, TFetchOn> source,
            Expression<Func<TFetchOn, TNestedFetch>> path) {
            var fetchRequest = (FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TNestedFetch>) source;
            return new FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TNestedFetch>(fetchRequest
                                                                                                          .Queryable);
        }

        public IFetchRequest<TOriginatingEntity, TNestedFetch>
            ThenFetchMany<TOriginatingEntity, TFetchOn, TNestedFetch>(
                IFetchRequest<TOriginatingEntity, TFetchOn> source,
                Expression<Func<TFetchOn, IEnumerable<TNestedFetch>>> path) {
            var fetchRequest = (FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TFetchOn>) source;
            return new FetchRequest<IQueryable<TOriginatingEntity>, TOriginatingEntity, TNestedFetch>(fetchRequest
                                                                                                          .Queryable);
        }
    }
}
