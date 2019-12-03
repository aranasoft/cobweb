using System;
using System.Linq;
using Cobweb.Data.NHibernate.Fetching;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("FetchingProvider")]
    public class FetchingDefaultProviderSpecs {
        [Fact]
        public void ItShouldUseTheDefaultFetchingProviderWhenSet() {
            EagerFetch.Current().Should().BeOfType<NHibernateFetchingProvider>();
        }

        [Fact]
        public void ItShouldThrowOnFetchWithFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().Fetch(root => root.Employer).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'Fetch' on type 'NHibernate.Linq.EagerFetchingExtensionMethods' that matches the specified arguments");
        }

        [Fact]
        public void ItShouldThrowOnFetchManyWithFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().FetchMany(root => root.Cars).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'FetchMany' on type 'NHibernate.Linq.EagerFetchingExtensionMethods' that matches the specified arguments");
        }
    }
}
