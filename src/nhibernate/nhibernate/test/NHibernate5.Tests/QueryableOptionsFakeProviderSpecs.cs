using System;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.QueryableOptions;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class QueryableOptionsFakeProviderSpecs : IDisposable {
        private readonly Func<IQueryableOptionsProvider> _currentQueryableOptionsProvider;

        public QueryableOptionsFakeProviderSpecs() {
            _currentQueryableOptionsProvider = QueryableOptionsProvider.Current;
            QueryableOptionsProvider.Current = () => new FakeQueryableOptionsProvider();
        }

        public void Dispose() {
            QueryableOptionsProvider.Current = _currentQueryableOptionsProvider;
        }

        [Fact]
        public void ItShouldUseTheFakeQueryableOptionsProviderWhenSet() {
            QueryableOptionsProvider.Current().Should().BeOfType<FakeQueryableOptionsProvider>();
        }

        [Fact]
        public void ItShouldNotThrowOnCacheableWithFakeQueryableOptionsProviderCacheableCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();
#pragma warning restore 618

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldThrowOnCacheModeWithDirectCacheModeCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.CacheMode(Enumerable.Empty<PersonEntity>().AsQueryable(), CacheMode.Normal).FirstOrDefault();
#pragma warning restore 618

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldThrowOnCacheRegionWithDirectCacheRegionCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.CacheRegion(Enumerable.Empty<PersonEntity>().AsQueryable(), "test").FirstOrDefault();
#pragma warning restore 618

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldThrowOnTimeoutWithDirectTimeoutCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.Timeout(Enumerable.Empty<PersonEntity>().AsQueryable(), 1000).FirstOrDefault();
#pragma warning restore 618

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldNotThrowOnCacheableWithFakeQueryableOptionsProviderSetOptionsCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.SetOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();
#pragma warning restore 618

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldNotThrowOnCacheableWithFakeQueryableOptionsProviderWithOptionsCall() {
            Action act = () => QueryableOptionsProvider.WithOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should().NotThrow();
        }

    }
}
