using System;
using System.Linq;
using Cobweb.Data.NHibernate.QueryableOptions;
using NHibernate.Linq;

namespace Cobweb.Testing.NHibernate.QueryableOptions {
    public class FakeQueryableOptionsProvider : IQueryableOptionsProvider {
        public IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions) {
            return source;
        }
    }
}
