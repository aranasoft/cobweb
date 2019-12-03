using Cobweb.DependencyInjection;
using FluentNHibernate.Cfg.Db;

namespace Cobweb.Data.NHibernate {
    public interface INHibernateConnectionConfiguration : ISingletonDependency {
        IPersistenceConfigurer Configuration();
    }
}
