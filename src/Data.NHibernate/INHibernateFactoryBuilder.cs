using Cobweb.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

namespace Cobweb.Data.NHibernate {
    public interface INHibernateFactoryBuilder : ISingletonDependency {
        ISessionFactory GetCurrentFactory();
        void CloseCurrentFactory();
        Configuration Configuration { get; }
    }
}