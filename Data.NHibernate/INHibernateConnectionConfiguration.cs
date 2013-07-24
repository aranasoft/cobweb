using Aranasoft.Cobweb.DependencyInjection;
using FluentNHibernate.Cfg.Db;

namespace Aranasoft.Cobweb.Data.NHibernate {
    public interface INHibernateConnectionConfiguration : ISingletonDependency {
        IPersistenceConfigurer Configuration();
    }
}