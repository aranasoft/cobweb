using System;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.QueryableOptions;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using Xunit;
#pragma warning disable 618

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class CachingFakeProviderSpecs : IDisposable {
        private readonly Func<IQueryableOptionsProvider> _currentCacheProvider;

        public CachingFakeProviderSpecs() {
            _currentCacheProvider = CachingProvider.Current;
            CachingProvider.Current = () => new FakeCachingProvider();
        }

        public void Dispose() {
            CachingProvider.Current = _currentCacheProvider;
        }

        [Fact]
        public void ItShouldUseTheFakeCachingProviderWhenSet() {
            CachingProvider.Current().Should().BeOfType<FakeCachingProvider>();
        }

        [Fact]
        public void ItShouldDeriveFromFakeQueryableOptionsPoviderWhenSet() {
            CachingProvider.Current().Should().BeAssignableTo<FakeQueryableOptionsProvider>();
        }

        [Fact]
        public void ItShouldNotThrowOnCacheableWithFakeCachingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().Cacheable().FirstOrDefault();

            act.Should().NotThrow();
        }
    }
}
