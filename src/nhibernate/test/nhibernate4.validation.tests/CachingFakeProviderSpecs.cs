using System;
using System.Linq;
using Cobweb.Data.NHibernate.Caching;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Testing.NHibernate.Caching;
using Cobweb.Testing.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace Cobweb.Testing.NHibernate.Tests {
    [Collection("CachingProvider")]
    public class CachingFakeProviderSpecs : IDisposable {
        private readonly Func<ICachingProvider> _currentCacheProvider;

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
        public void ItShouldNotThrowOnCacheableWithFakeCachingProviderCall() {
            Action act = () => CachingProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should().NotThrow();
        }
    }
}
