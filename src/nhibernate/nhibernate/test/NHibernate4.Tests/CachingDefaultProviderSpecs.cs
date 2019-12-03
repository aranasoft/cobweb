using System;
using System.Linq;
using Cobweb.Data.NHibernate.Caching;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("CachingProvider")]
    public class CachingDefaultProviderSpecs {
        [Fact]
        public void ItShouldUseTheDefaultCachingProviderWhenSet() {
            CachingProvider.Current().Should().BeOfType<NHibernateCachingProvider>();
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithCachingProviderCall() {
            Action act = () => CachingProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'Cacheable' on type 'NHibernate.Linq.LinqExtensionMethods' that matches the specified arguments");
        }
    }
}
