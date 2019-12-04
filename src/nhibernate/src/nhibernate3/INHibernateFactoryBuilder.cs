using Aranasoft.Cobweb.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

namespace Aranasoft.Cobweb.NHibernate {
    public interface INHibernateFactoryBuilder : ISingletonDependency {
        Configuration Configuration { get; }
        ISessionFactory GetCurrentFactory();
        void CloseCurrentFactory();
    }
}
