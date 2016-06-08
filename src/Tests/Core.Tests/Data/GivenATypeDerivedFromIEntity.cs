using System;
using Cobweb.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Data {
    [TestFixture]
    public class GivenATypeDerivedFromIEntity {
        private class SampleInterfaceEntity : IEntity<SampleInterfaceEntity>, IEquatable<SampleInterfaceEntity> {
            public bool Equals(SampleInterfaceEntity other) {
                return !ReferenceEquals(null, other) && ReferenceEquals(this, other);
            }
        }

        [Test]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.Current.IsEntity(typeof(SampleInterfaceEntity)).Should().BeTrue();
        }
    }
}
