using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using NHibernate;

namespace Cobweb.Data.NHibernate.Tests {
    /// <summary>
    ///     These tests are not executed by NUnit. They are here for compile-time checking. 'If it builds, ship it.'
    /// </summary>
    public class CompileTests {
        internal class ExtensionMethodCompiler {
            public void ShouldCompile() {
                var oneOneMany = new Collection<CarEntity>().AsQueryable()
                                                            .Fetch(carEntity => carEntity.Owner)
                                                            .ThenFetch(personEntity => personEntity.Employer)
                                                            .ThenFetchMany(employerEntity => employerEntity.Employees);
                var oneManyOne = new Collection<PersonEntity>().AsQueryable()
                                                               .Fetch(thing => thing.Employer)
                                                               .ThenFetchMany(child => child.Employees)
                                                               .ThenFetch(parent => parent.Representative);
                var oneManyMany = new Collection<PersonEntity>().AsQueryable()
                                                                .Fetch(thing => thing.Employer)
                                                                .ThenFetchMany(child => child.Employees)
                                                                .ThenFetchMany(parent => parent.Cars);
                var manyOneMany = new Collection<PersonEntity>().AsQueryable()
                                                                .FetchMany(thing => thing.Pets)
                                                                .ThenFetch(child => child.Owner)
                                                                .ThenFetchMany(parent => parent.Cars);
                var manyManyOne = new Collection<EmployerEntity>().AsQueryable()
                                                                  .FetchMany(thing => thing.Employees)
                                                                  .ThenFetchMany(child => child.Cars)
                                                                  .ThenFetch(parent => parent.Owner);
                var manyOneOne = new Collection<PersonEntity>().AsQueryable()
                                                               .FetchMany(thing => thing.Cars)
                                                               .ThenFetch(child => child.Owner)
                                                               .ThenFetch(parent => parent.Employer);
            }
        }

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
