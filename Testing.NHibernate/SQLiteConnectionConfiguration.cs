using Aranasoft.Cobweb.Data.NHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;

namespace Aranasoft.Cobweb.Testing.NHibernate {
    public class SqLiteInMemoryConnectionConfiguration : INHibernateConnectionConfiguration {
        public IPersistenceConfigurer Configuration<TDialect>() where TDialect : SQLiteDialect {
            return SQLiteConfiguration.Standard.InMemory().Dialect<TDialect>().QuerySubstitutions("true=1;false=0");
        }

        public IPersistenceConfigurer Configuration() {
            return Configuration<SQLiteDialect>();
        }
    }
}