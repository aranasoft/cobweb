using NHibernate;

namespace Cobweb.Data.NHibernate {
    public interface INHibernateSessionBuilder : IDataTransactionManager {
        ISession GetCurrentSession();
    }

    public abstract class NHibernateSessionBuilder : DataTransactionManager, INHibernateSessionBuilder {
        public abstract ISession GetCurrentSession();
    }
}
