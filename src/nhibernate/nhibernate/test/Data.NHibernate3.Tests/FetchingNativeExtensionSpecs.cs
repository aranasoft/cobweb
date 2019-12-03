using System;
using System.Linq;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using NHibernate.Linq;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("FetchingProvider")]
    public class FetchingNativeExtensionSpecs {
        [Fact]
        public void ItShouldThrowOnFetchWithDirectFetchCall() {
            Action act = () => EagerFetchingExtensionMethods.Fetch(Enumerable.Empty<PersonEntity>().AsQueryable(), root => root.Employer).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'Fetch' on type 'NHibernate.Linq.EagerFetchingExtensionMethods' that matches the specified arguments");
        }

        [Fact]
        public void ItShouldThrowOnFetchManyWithDirectFetchCall() {
            Action act = () => EagerFetchingExtensionMethods.FetchMany(Enumerable.Empty<PersonEntity>().AsQueryable(), root => root.Cars).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'FetchMany' on type 'NHibernate.Linq.EagerFetchingExtensionMethods' that matches the specified arguments");
        }
    }
}
