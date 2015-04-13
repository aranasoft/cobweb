using System;
using System.Data;
using System.Linq;
using NHibernate;

namespace Cobweb.Data.NHibernate.Tests {
    /// <summary>
    ///     These tests are not executed by NUnit. They are here for compile-time checking. 'If it builds, ship it.'
    /// </summary>
    public class CompileTests {
        internal class SessionBuilder : NHibernateSessionBuilder {
            public override IDataTransaction BeginTransaction() {
                return new NHibernateTransactionHandler(GetCurrentSession().BeginTransaction());
            }

            public override IDataTransaction BeginTransaction(IsolationLevel isolationLevel) {
                return new NHibernateTransactionHandler(GetCurrentSession().BeginTransaction());
            }

            public override ISession GetCurrentSession() {
                throw new NotImplementedException();
            }
        }
    }
}
