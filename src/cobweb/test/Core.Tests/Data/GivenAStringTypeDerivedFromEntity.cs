using System;
using Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Data {
    public class GivenAStringTypeDerivedFromEntity {
        private class StringEntity : Entity<StringEntity, string>, IEquatable<StringEntity> {}

        [Fact]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.Current.IsEntity(typeof(StringEntity)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldConsiderSameInstanceEqual() {
            var entity1 = new StringEntity();
            var entity2 = entity1;

            entity1.Should().Be(entity2);
        }

        [Fact]
        public void ItShouldConsiderSameDefaultIdentityNotEqual() {
            var entity1 = new StringEntity {Id = null};
            var entity2 = new StringEntity {Id = null};

            entity1.Should().NotBe(entity2);
        }

        [Fact]
        public void ItShouldConsiderSameDefaultIdentityNotEqualViaEqualOperator() {
            var entity1 = new StringEntity {Id = null};
            var entity2 = new StringEntity {Id = null};

            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldConsiderSameDefaultIdentityNotEqualViaUnequalOperator() {
            var entity1 = new StringEntity {Id = null};
            var entity2 = new StringEntity {Id = null};

            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqual() {
            var identity = "1234qwerasdf";
            var entity1 = new StringEntity {Id = identity};
            var entity2 = new StringEntity {Id = identity};

            entity1.Should().Be(entity2);
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqualViaEqualOperator() {
            var identity = "1234qwerasdf";
            var entity1 = new StringEntity {Id = identity};
            var entity2 = new StringEntity {Id = identity};

            (entity1 == entity2).Should().BeTrue();
        }

        [Fact]
        public void ItShouldConsiderSameNonDefaultIdentityEqualViaUnequalOperator() {
            var identity = "1234qwerasdf";
            var entity1 = new StringEntity {Id = identity};
            var entity2 = new StringEntity {Id = identity};

            (entity1 != entity2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqual() {
            var entity1 = new StringEntity {Id = "12345"};
            var entity2 = new StringEntity {Id = "qwert"};

            entity1.Should().NotBe(entity2);
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqualViaEqualOperator() {
            var entity1 = new StringEntity {Id = "12345"};
            var entity2 = new StringEntity {Id = "qwert"};

            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotConsiderDifferentIdentityEqualViaUnequalOperator() {
            var entity1 = new StringEntity {Id = "12345"};
            var entity2 = new StringEntity {Id = "qwert"};

            (entity1 != entity2).Should().BeTrue();
        }
    }
}
