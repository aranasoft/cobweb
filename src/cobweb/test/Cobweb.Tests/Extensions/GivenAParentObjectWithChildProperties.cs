using Cobweb.Extentions.ObjectExtentions;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Extensions {
    public class GivenAParentObjectWithChildProperties {
        public class ParentThing {
            public int ChildThing {
                get { return 27; }
            }
        }

        public class GivenANullParent {
            private ParentThing _parent;

            public GivenANullParent() {
                _parent = default(ParentThing);
            }

            [Fact]
            public void IfExistsShouldReturnTheDefaultTypeForTheChildProprty() {
                _parent.IfExists(parent => parent.ChildThing).Should().Be(default(int));
            }

            [Fact]
            public void IfExistsShouldReturnTheDefaultValueIfSpecified() {
                _parent.IfExists(parent => parent.ChildThing, 101).Should().Be(101);
            }
        }

        public class GivenAnInstanceOfParent {
            private ParentThing _parent;

            public GivenAnInstanceOfParent() {
                _parent = new ParentThing();
            }

            [Fact]
            public void IfExistsShouldReturnTheValueOfTheChildProperty() {
                _parent.IfExists(parent => parent.ChildThing).Should().Be(27);
            }
        }
    }
}
