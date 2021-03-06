﻿using System.Linq;
using NHibernate.Linq;

namespace Aranasoft.Cobweb.NHibernate.Caching {
    public class NHibernateCachingProvider : ICachingProvider {
        public ICacheRequest<T> Cacheable<T>(
            IQueryable<T> source) {
            var cachedQuery = source.Cacheable();
            return new NHibernateCacheRequest<T>(cachedQuery);
        }
    }
}
