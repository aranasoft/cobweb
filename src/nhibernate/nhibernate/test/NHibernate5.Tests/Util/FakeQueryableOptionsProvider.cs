using System;
using System.Linq;
using Cobweb.Data.NHibernate.QueryableOptions;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class FakeQueryableOptionsProvider : IQueryableOptionsProvider {
        public IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions) {
            return source;
        }
    }
}
