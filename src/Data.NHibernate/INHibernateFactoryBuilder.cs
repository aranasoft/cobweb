using Cobweb.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

namespace Cobweb.Data.NHibernate {
    public interface INHibernateFactoryBuilder : ISingletonDependency {
        Configuration Configuration { get; }
        ISessionFactory GetCurrentFactory();
        void CloseCurrentFactory();
    }
}
