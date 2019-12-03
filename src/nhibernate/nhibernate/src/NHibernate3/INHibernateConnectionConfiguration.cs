using Cobweb.DependencyInjection;
using FluentNHibernate.Cfg.Db;

namespace Aranasoft.Cobweb.NHibernate {
    public interface INHibernateConnectionConfiguration : ISingletonDependency {
        IPersistenceConfigurer Configuration();
    }
}
