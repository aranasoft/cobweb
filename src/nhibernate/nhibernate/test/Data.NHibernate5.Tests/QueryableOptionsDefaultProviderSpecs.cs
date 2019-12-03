using System;
using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.QueryableOptions;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("QueryableOptionsProvider")]
    public class QueryableOptionsDefaultProviderSpecs {
        [Fact]
        public void ItShouldUseTheDefaultQueryableOptionsProviderWhenSet() {
            QueryableOptionsProvider.Current().Should().BeOfType<NHibernateQueryableOptionsProvider>();
        }
 
        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderCacheableCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();
#pragma warning restore 618

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheModeWithDirectCacheModeCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.CacheMode(Enumerable.Empty<PersonEntity>().AsQueryable(), CacheMode.Normal).FirstOrDefault();
#pragma warning restore 618

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheRegionWithDirectCacheRegionCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.CacheRegion(Enumerable.Empty<PersonEntity>().AsQueryable(), "test").FirstOrDefault();
#pragma warning restore 618

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnTimeoutWithDirectTimeoutCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.Timeout(Enumerable.Empty<PersonEntity>().AsQueryable(), 1000).FirstOrDefault();
#pragma warning restore 618

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderSetOptionsCall() {
#pragma warning disable 618
            Action act = () => QueryableOptionsProvider.SetOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();
#pragma warning restore 618

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }

        [Fact]
        public void ItShouldThrowOnCacheableWithQueryableOptionsProviderWithOptionsCall() {
            Action act = () => QueryableOptionsProvider.WithOptions(Enumerable.Empty<PersonEntity>().AsQueryable(), options => options.SetCacheable(true)).FirstOrDefault();

            act.Should()
               .Throw<NotSupportedException>()
               .WithMessage(
                   "The query.Provider does not support setting options. Please implement IQueryProviderWithOptions.");
        }
    }
}
