using Cobweb.Extentions;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Extensions {
    public class GivenABaseTypeThatIsGeneric {
        public class Animal<T> where T : Animal<T> {}

        [Fact]
        public void ItShouldNotIdentifyANonDerivedTypeAsDerived() {
            typeof(object).IsAssignableToGeneric(typeof(Animal<>)).Should().BeFalse();
        }

    
        public class GivenADerivedTypeThatIsNonGeneric {
            public class Fish : Animal<Fish> {}

            [Fact]
            public void ItShouldIdentifyTheDerivedTypeAsDerived() {
                typeof(Fish).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }
        }

    
        public class GivenADerivedTypeThatIsGeneric {
            public class FlyingAnimal<T> : Animal<FlyingAnimal<T>> {}

            [Fact]
            public void ItShouldIdentifyTheDerivedTypeAsDerived() {
                typeof(FlyingAnimal<>).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }
        }
    }
}
