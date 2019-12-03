using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public abstract class SqLiteNHibernateTest : IClassFixture<SqLiteNHibernateFixture>, IDisposable {
        protected Configuration SessionConfiguration { get; }
        protected ISessionFactory SessionFactory;
        protected ISession Session;

        protected SqLiteNHibernateTest(SqLiteNHibernateFixture fixture) {
            SessionConfiguration = fixture.SessionConfiguration;
            SessionFactory = SessionConfiguration.SetProperty("generate_statistics", true.ToString()).BuildSessionFactory();
            Session = SessionFactory.OpenSession();
            Session.FlushMode = FlushMode.Commit;

            var schemaExport = new SchemaExport(SessionConfiguration);
            schemaExport.Execute(false, true, false, Session.Connection, null);
        }

        public void Dispose() {
            if (Session.IsOpen) Session.Close();
            Session.Dispose();
        }
    }
}