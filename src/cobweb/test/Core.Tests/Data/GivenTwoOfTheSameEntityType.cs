using System;
using Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Data {
    public class GivenTwoOfTheSameEntityType {
        private class SampleEntity : Entity<SampleEntity, Guid>, IEquatable<SampleEntity> {}

        [Fact]
        public void ItShouldConsiderSameInstanceEqual() {
            var entity1 = new SampleEntity();
            var entity2 = entity1;

            entity1.Should().Be(entity2);
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqual() {
            var identity = Guid.NewGuid();
            var entity1 = new SampleEntity {Id = identity};
            var entity2 = new SampleEntity {Id = identity};

            entity1.Should().Be(entity2);
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqualViaEqualOperator() {
            var identity = Guid.NewGuid();
            var entity1 = new SampleEntity {Id = identity};
            var entity2 = new SampleEntity {Id = identity};

            (entity1 == entity2).Should().BeTrue();
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqualViaUnequalOperator() {
            var identity = Guid.NewGuid();
            var entity1 = new SampleEntity {Id = identity};
            var entity2 = new SampleEntity {Id = identity};

            (entity1 != entity2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqual() {
            var entity1 = new SampleEntity {Id = Guid.NewGuid()};
            var entity2 = new SampleEntity {Id = Guid.NewGuid()};

            entity1.Should().NotBe(entity2);
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqualViaEqualOperator() {
            var entity1 = new SampleEntity {Id = Guid.NewGuid()};
            var entity2 = new SampleEntity {Id = Guid.NewGuid()};

            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqualViaUnequalOperator() {
            var entity1 = new SampleEntity {Id = Guid.NewGuid()};
            var entity2 = new SampleEntity {Id = Guid.NewGuid()};

            (entity1 != entity2).Should().BeTrue();
        }
    }
}
