using System;
using Aranasoft.Cobweb.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Tests.Data {
    [TestFixture]
    public class GivenATypeDerivedFromEntity {
        private class SampleEntity : Entity<SampleEntity, Guid>, IEquatable<SampleEntity> {}

        [Test]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.IsEntity(typeof (SampleEntity)).Should().BeTrue();
        }
    }
}