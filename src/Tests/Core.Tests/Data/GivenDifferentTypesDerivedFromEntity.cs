using System;
using Cobweb.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Data {
    [TestFixture]
    public class GivenDifferentTypesDerivedFromEntity {
        private class SampleAEntity : Entity<SampleAEntity, Guid>, IEquatable<SampleAEntity> {}

        private class SampleBEntity : Entity<SampleBEntity, Guid>, IEquatable<SampleBEntity> {}

        [Test]
        public void ItShouldNotConsiderSameIdentityEqual() {
            var identity = Guid.NewGuid();
            var entity1 = new SampleAEntity {Id = identity};
            var entity2 = new SampleBEntity {Id = identity};

            entity1.Should().NotBe(entity2);
        }

        [Test]
        public void ItShouldNotConsiderDifferentIdentityEqual() {
            var entity1 = new SampleAEntity {Id = Guid.NewGuid()};
            var entity2 = new SampleBEntity {Id = Guid.NewGuid()};

            entity1.Should().NotBe(entity2);
        }
    }
}
