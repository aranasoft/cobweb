using System;
using System.Linq;
using NHibernate.Linq;

namespace Aranasoft.Cobweb.NHibernate.QueryableOptions {
    public interface IQueryableOptionsProvider {
        IQueryable<T> WithOptions<T>(IQueryable<T> source, Action<NhQueryableOptions> setOptions);
    }
}
