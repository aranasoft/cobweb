using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.NHibernate.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.NHibernate.Tests {
    public class EntityTypeSourceSpecs {
        private readonly EntityTypeSource _entityTypeSource;

        public EntityTypeSourceSpecs() {
            var assemblies = new List<Assembly> {GetType().Assembly};
            _entityTypeSource = new EntityTypeSource(assemblies);
        }

        [Fact]
        public void ItShouldContainCorrectTypeCount() {
            _entityTypeSource.GetTypes().Count().Should().Be(5);
        }

        [Fact]
        public void ItShouldContainPersonEntityType() {
            _entityTypeSource.GetTypes().Should().Contain(typeof(CarEntity));
        }

        [Fact]
        public void ItShouldContainChildEntityType() {
            _entityTypeSource.GetTypes().Should().Contain(typeof(PersonEntity));
        }
    }
}
