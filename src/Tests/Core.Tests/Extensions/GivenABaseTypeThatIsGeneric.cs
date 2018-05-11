using Cobweb.Extentions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenABaseTypeThatIsGeneric {
        public class Animal<T> where T : Animal<T> {}

        [Test]
        public void ItShouldNotIdentifyANonDerivedTypeAsDerived() {
            typeof(object).IsAssignableToGeneric(typeof(Animal<>)).Should().BeFalse();
        }

        [TestFixture]
        public class GivenADerivedTypeThatIsNonGeneric {
            public class Fish : Animal<Fish> {}

            [Test]
            public void ItShouldIdentifyTheDerivedTypeAsDerived() {
                typeof(Fish).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }
        }

        [TestFixture]
        public class GivenADerivedTypeThatIsGeneric {
            public class FlyingAnimal<T> : Animal<FlyingAnimal<T>> {}

            [Test]
            public void ItShouldIdentifyTheDerivedTypeAsDerived() {
                typeof(FlyingAnimal<>).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }
        }
    }
}
