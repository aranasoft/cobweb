using System;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.QueryableOptions;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;
#pragma warning disable 618

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class CachingDefaultProviderSpecs {
        [Fact]
        public void ItShouldUseTheDefaultQueryableOptionsProviderWhenSet() {
            CachingProvider.Current().Should().BeOfType<NHibernateQueryableOptionsProvider>();
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithCachingProviderCall() {
            Action act = () => CachingProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }
    }
}
