using Aranasoft.Cobweb.NHibernate.Validation.Dialect;
using FluentNHibernate.Cfg.Db;

namespace Aranasoft.Cobweb.NHibernate.Validation {
    public class FluentMigratorSQLiteInMemoryConnectionConfiguration : INHibernateConnectionConfiguration {
        public IPersistenceConfigurer Configuration() {
            return
                SQLiteConfiguration.Standard.InMemory()
                                   .Dialect<FluentMigratorSQLiteDialect>()
                                   .QuerySubstitutions("true=1;false=0");
        }
    }
}
