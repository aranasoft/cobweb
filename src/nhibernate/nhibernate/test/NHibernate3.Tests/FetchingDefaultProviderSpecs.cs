using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.Fetching;
using Aranasoft.Cobweb.NHibernate.Providers;
using Aranasoft.Cobweb.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.NHibernate.Tests {
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
