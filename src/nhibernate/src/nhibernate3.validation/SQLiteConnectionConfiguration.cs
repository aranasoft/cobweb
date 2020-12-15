using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;

namespace Aranasoft.Cobweb.NHibernate.Validation {
    public class SQLiteInMemoryConnectionConfiguration : INHibernateConnectionConfiguration {
        public IPersistenceConfigurer Configuration() {
            return Configuration<SQLiteDialect>();
        }

        public IPersistenceConfigurer Configuration<TDialect>() where TDialect : SQLiteDialect {
            return SQLiteConfiguration.Standard.InMemory().Dialect<TDialect>().QuerySubstitutions("true=1;false=0");
        }
    }
}
