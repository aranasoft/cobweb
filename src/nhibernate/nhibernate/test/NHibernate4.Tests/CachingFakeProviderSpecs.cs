using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.Caching;
using Aranasoft.Cobweb.NHibernate.Providers;
using Aranasoft.Cobweb.NHibernate.Tests.Entities;
using Aranasoft.Cobweb.NHibernate.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.NHibernate.Tests {
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
