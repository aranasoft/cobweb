using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.Providers;
using Aranasoft.Cobweb.NHibernate.QueryableOptions;
using Aranasoft.Cobweb.NHibernate.Validation.Tests.Entities;
using FluentAssertions;
using Xunit;
#pragma warning disable 618

namespace Aranasoft.Cobweb.NHibernate.Validation.Tests {
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
