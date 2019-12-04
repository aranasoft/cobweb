using Cobweb.Data;
using NHibernate;

namespace Aranasoft.Cobweb.NHibernate {
    public interface INHibernateSessionBuilder : IDataTransactionManager {
        ISession GetCurrentSession();
    }

    public abstract class NHibernateSessionBuilder : DataTransactionManager, INHibernateSessionBuilder {
        public abstract ISession GetCurrentSession();
    }
}
