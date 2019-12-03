using System;
using Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Data {
    public class GivenATypeDerivedFromIEntity {
        private class SampleInterfaceEntity : IEntity<SampleInterfaceEntity>, IEquatable<SampleInterfaceEntity> {
            public bool Equals(SampleInterfaceEntity other) {
                return !ReferenceEquals(null, other) && ReferenceEquals(this, other);
            }
        }

        [Fact]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.Current.IsEntity(typeof(SampleInterfaceEntity)).Should().BeTrue();
        }
    }
}
