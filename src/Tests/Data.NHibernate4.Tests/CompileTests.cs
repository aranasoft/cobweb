using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using NHibernate;

namespace Cobweb.Data.NHibernate.Tests {
    /// <summary>
    ///     These tests are not executed by NUnit. They are here for compile-time checking. 'If it builds, ship it.'
    /// </summary>
    public class CompileTests {
        internal class SampleParentEntity {
            public SampleChildEntity Child { get; set; }
            public IOrderedEnumerable<SampleChildEntity> Children { get; set; }
        }

        internal class SampleChildEntity {
            public SampleParentEntity Parent { get; set; }
            public IOrderedEnumerable<SampleParentEntity> Parents { get; set; }
        }

        internal class ExtensionMethodCompiler {
            public void ShouldCompile() {
                var fetchThenFetchThenFetch = new Collection<SampleParentEntity>() {}
                                              .AsQueryable()
                                              .Fetch(thing => thing.Child)
                                              .ThenFetch(child => child.Parent)
                                              .ThenFetch(parent => parent.Child);
                var fetchThenFetchManyThenFetch = new Collection<SampleParentEntity>() {}
                                                  .AsQueryable()
                                                  .Fetch(thing => thing.Child)
                                                  .ThenFetchMany(child => child.Parents)
                                                  .ThenFetch(parent => parent.Child);
                var fetchManyThenFetchThenFetchMany = new Collection<SampleParentEntity>() {}
                                                      .AsQueryable()
                                                      .FetchMany(thing => thing.Children)
                                                      .ThenFetch(child => child.Parent)
                                                      .ThenFetchMany(parent => parent.Children);
                var fetchManyThenFetchManyThenFetchMany = new Collection<SampleParentEntity>() {}
                                                          .AsQueryable()
                                                          .FetchMany(thing => thing.Children)
                                                          .ThenFetchMany(child => child.Parents)
                                                          .ThenFetchMany(parent => parent.Children);
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
