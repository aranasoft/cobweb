using System;
using System.Linq;
using Cobweb.Data.NHibernate.Tests.Entities;
using FluentAssertions;
using NHibernate.Linq;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("CachingProvider")]
    public class CachingNativeExtensionSpecs {
        [Fact]
        public void ItShouldThrowOnCacheableWithDirectCacheableCall() {
            Action act = () => LinqExtensionMethods.Cacheable(Enumerable.Empty<PersonEntity>().AsQueryable()).FirstOrDefault();

            act.Should()
               .Throw<InvalidOperationException>()
               .WithMessage(
                   "There is no method 'Cacheable' on type 'NHibernate.Linq.LinqExtensionMethods' that matches the specified arguments");
        }
    }
}
