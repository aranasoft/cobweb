using System;
using System.Linq;
using NHibernate.Linq;

namespace Cobweb.Data.NHibernate.QueryableOptions {
    public interface IQueryableOptionsProvider {
        IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions);
    }
}
