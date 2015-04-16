using Cobweb.Extentions.ObjectExtentions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenAParentObjectWithChildProperties {
        public class ParentThing {
            public int ChildThing {
                get { return 27; }
            }
        }

        [TestFixture]
        public class GivenANullParent {
            private ParentThing _parent;

            [SetUp]
            public void SetUp() {
                _parent = default(ParentThing);
            }

            [Test]
            public void IfExistsShouldReturnTheDefaultTypeForTheChildProprty() {
                _parent.IfExists(parent => parent.ChildThing).Should().Be(default(int));
            }

            [Test]
            public void IfExistsShouldReturnTheDefaultValueIfSpecified() {
                _parent.IfExists(parent => parent.ChildThing, 101).Should().Be(101);
            }
        }

        [TestFixture]
        public class GivenAnInstanceOfParent {
            private ParentThing _parent;

            [SetUp]
            public void SetUp() {
                _parent = new ParentThing();
            }

            [Test]
            public void IfExistsShouldReturnTheValueOfTheChildProperty() {
                _parent.IfExists(parent => parent.ChildThing).Should().Be(27);
            }
        }
    }
}
