using System;
using Aranasoft.Cobweb.Data;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Tests.Data;
public class GivenATypeDerivedFromEntity {
    private class SampleEntity : Entity<SampleEntity, Guid>, IEquatable<SampleEntity> {}

    [Fact]
    public void ItShouldIdentifyAsDerivedFromEntity() {
        EntityManager.Current.IsEntity(typeof(SampleEntity)).Should().BeTrue();
    }
}
