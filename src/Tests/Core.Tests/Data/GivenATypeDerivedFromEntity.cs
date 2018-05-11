using System;
using Cobweb.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Data {
    [TestFixture]
    public class GivenATypeDerivedFromEntity {
        private class SampleEntity : Entity<SampleEntity, Guid>, IEquatable<SampleEntity> {}

        [Test]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.Current.IsEntity(typeof(SampleEntity)).Should().BeTrue();
        }
    }
}
