using Cobweb.Data.NHibernate;
using Cobweb.Testing.NHibernate.Dialect;
using FluentNHibernate.Cfg.Db;

namespace Cobweb.Testing.NHibernate {
    public class FluentMigratorSQLiteInMemoryConnectionConfiguration : INHibernateConnectionConfiguration {
        public IPersistenceConfigurer Configuration() {
            return
                SQLiteConfiguration.Standard.InMemory()
                                   .Dialect<FluentMigratorSQLiteDialect>()
                                   .QuerySubstitutions("true=1;false=0");
        }
    }
}
