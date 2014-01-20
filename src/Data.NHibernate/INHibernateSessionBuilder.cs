using NHibernate;

namespace Cobweb.Data.NHibernate {
    public interface INHibernateSessionBuilder : IDataTransactionManager {
        ISession GetCurrentSession();
    }
}
