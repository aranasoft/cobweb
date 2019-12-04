using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.QueryableOptions;
using NHibernate.Linq;

namespace Aranasoft.Cobweb.NHibernate.Validation.QueryableOptions {
    public class FakeQueryableOptionsProvider : IQueryableOptionsProvider {
        public IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions) {
            return source;
        }
    }
}
