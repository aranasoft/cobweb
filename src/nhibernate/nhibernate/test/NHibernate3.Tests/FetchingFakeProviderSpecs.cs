using System;
using System.Linq;
using Aranasoft.Cobweb.NHibernate.Fetching;
using Aranasoft.Cobweb.NHibernate.Providers;
using Aranasoft.Cobweb.NHibernate.Tests.Entities;
using Aranasoft.Cobweb.NHibernate.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.NHibernate.Tests {
    [Collection("FetchingProvider")]
    public class FetchingFakeProviderSpecs : IDisposable {
        private readonly Func<IFetchingProvider> _currentFetchProvider;
        
        public FetchingFakeProviderSpecs() {
            _currentFetchProvider = EagerFetch.Current;
            EagerFetch.Current = () => new FakeFetchingProvider();
        }

        public void Dispose() {
            EagerFetch.Current = _currentFetchProvider;
        }

        [Fact]
        public void ItShouldUseTheFakeFetchingProviderWhenSet() {
            EagerFetch.Current().Should().BeOfType<FakeFetchingProvider>();
        }

        [Fact]
        public void ItShouldNotThrowOnFetchWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().Fetch(root => root.Employer).FirstOrDefault();

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldNotThrowOnFetchManyWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().FetchMany(root => root.Cars).FirstOrDefault();

            act.Should().NotThrow();
        }
        
        [Fact]
        public void ItShouldNotThrowOnFetchThenFetchWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<CarEntity>().AsQueryable().Fetch(root => root.Owner).ThenFetch(child => child.Employer).FirstOrDefault();

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldNotThrowOnFetchThenFetchManyWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().Fetch(root => root.Employer).ThenFetchMany(child => child.Employees).FirstOrDefault();

            act.Should().NotThrow();
        }
        
        [Fact]
        public void ItShouldNotThrowOnFetchManyThenFetchWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<PersonEntity>().AsQueryable().FetchMany(root => root.Cars).ThenFetch(child => child.Owner).FirstOrDefault();

            act.Should().NotThrow();
        }

        [Fact]
        public void ItShouldNotThrowOnFetchManyThenFetchManyWithFakeFetchingProviderCall() {
            Action act = () => Enumerable.Empty<EmployerEntity>().AsQueryable().FetchMany(root => root.Employees).ThenFetchMany(child => child.Cars).FirstOrDefault();

            act.Should().NotThrow();
        }
    }
}
