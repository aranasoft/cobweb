using Aranasoft.Cobweb.Data.NHibernate;
using Aranasoft.Cobweb.Testing.NHibernate.Dialect;
using FluentNHibernate.Cfg.Db;

namespace Aranasoft.Cobweb.Testing.NHibernate {
    public class FluentMigratorSqLiteInMemoryConnectionConfiguration : INHibernateConnectionConfiguration {
        public IPersistenceConfigurer Configuration() {
            return
                SQLiteConfiguration.Standard.InMemory()
                                   .Dialect<FluentMigratorSQLiteDialect>()
                                   .QuerySubstitutions("true=1;false=0");
        }
    }
}