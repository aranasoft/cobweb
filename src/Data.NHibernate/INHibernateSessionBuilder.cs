using NHibernate;

namespace Aranasoft.Cobweb.Data.NHibernate
{
    public interface INHibernateSessionBuilder : IDataTransactionManager
    {
        ISession GetCurrentSession();
    }
}