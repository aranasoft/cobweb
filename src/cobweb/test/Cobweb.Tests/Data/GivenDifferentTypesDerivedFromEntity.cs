using System;
using Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Data {
    public class GivenDifferentTypesDerivedFromEntity {
        private class SampleAEntity : Entity<SampleAEntity, Guid>, IEquatable<SampleAEntity> {}

        private class SampleBEntity : Entity<SampleBEntity, Guid>, IEquatable<SampleBEntity> {}

        [Fact]
        public void ItShouldNotConsiderSameIdentityEqual() {
            var identity = Guid.NewGuid();
            var entity1 = new SampleAEntity {Id = identity};
            var entity2 = new SampleBEntity {Id = identity};

            entity1.Should().NotBe(entity2);
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqual() {
            var entity1 = new SampleAEntity {Id = Guid.NewGuid()};
            var entity2 = new SampleBEntity {Id = Guid.NewGuid()};

            entity1.Should().NotBe(entity2);
        }
    }
}
