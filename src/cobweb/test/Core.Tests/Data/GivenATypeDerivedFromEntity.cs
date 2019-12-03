using System;
using Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Data {
    public class GivenATypeDerivedFromEntity {
        private class SampleEntity : Entity<SampleEntity, Guid>, IEquatable<SampleEntity> {}

        [Fact]
        public void ItShouldIdentifyAsDerivedFromEntity() {
            EntityManager.Current.IsEntity(typeof(SampleEntity)).Should().BeTrue();
        }
    }
}
