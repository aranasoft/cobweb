using System;
using System.Linq;
using Cobweb.Data.NHibernate.Fetching;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
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
