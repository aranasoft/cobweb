using Aranasoft.Cobweb.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

namespace Aranasoft.Cobweb.Data.NHibernate {
    public interface INHibernateFactoryBuilder : ISingletonDependency {
        ISessionFactory GetCurrentFactory();
        void CloseCurrentFactory();
        Configuration Configuration { get; }
    }
}