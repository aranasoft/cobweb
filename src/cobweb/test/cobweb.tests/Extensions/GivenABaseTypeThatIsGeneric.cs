using System;
using Aranasoft.Cobweb.Extentions;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Tests.Extensions {
    public class GivenABaseTypeThatIsGeneric {
        public class Animal<T> where T : Animal<T> {}

        [Fact]
        public void ItShouldNotIdentifyANonDerivedTypeAsDerived() {
            typeof(object).IsAssignableToGeneric(typeof(Animal<>)).Should().BeFalse();
        }

        public class GivenAChildTypeThatIsNonGeneric {
            public class Fish : Animal<Fish> {}

            [Fact]
            public void ItShouldIdentifyTheParentTypeAsDerived() {
                typeof(Fish).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }

            [Fact]
            public void ItShouldIdentifyAnUnrelatedTypeAsNotDerived() {
                typeof(Fish).IsAssignableToGeneric(typeof(Nullable<>)).Should().BeFalse();
            }

            [Fact]
            public void ItShouldIdentifyTheGenericParentTypeDefinition() {
                typeof(Fish).GetGenericParentType(typeof(Animal<>)).Should().Be(typeof(Animal<Fish>));
            }
        }

        public class GivenAChildTypeThatIsGeneric {
            public class FlyingAnimal<T> : Animal<FlyingAnimal<T>> {}

            [Fact]
            public void ItShouldIdentifyTheParentTypeAsDerived() {
                typeof(FlyingAnimal<>).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
            }

            [Fact]
            public void ItShouldIdentifyAnUnrelatedTypeAsNotDerived() {
                typeof(FlyingAnimal<>).IsAssignableToGeneric(typeof(Nullable<>)).Should().BeFalse();
            }

            [Fact]
            public void ItShouldIdentifyTheGenericParentTypeDefinition() {
                typeof(FlyingAnimal<object>).GetGenericParentType(typeof(Animal<>)).Should().Be(typeof(Animal<FlyingAnimal<object>>));
            }

            [Fact]
            public void ItShouldIdentifyTheGenericSelfTypeDefinition() {
                typeof(FlyingAnimal<object>).GetGenericParentType(typeof(FlyingAnimal<>)).Should().Be(typeof(FlyingAnimal<object>));
            }

            public class GivenAGrandchildTypeThatIsNonGeneric {
                public class Bird : FlyingAnimal<Bird> {}

                [Fact]
                public void ItShouldIdentifyTheParentTypeAsDerived() {
                    typeof(Bird).IsAssignableToGeneric(typeof(FlyingAnimal<>)).Should().BeTrue();
                }

                [Fact]
                public void ItShouldIdentifyTheGrandparentTypeAsDerived() {
                    typeof(Bird).IsAssignableToGeneric(typeof(Animal<>)).Should().BeTrue();
                }

                [Fact]
                public void ItShouldIdentifyAnUnrelatedTypeAsNotDerived() {
                    typeof(Bird).IsAssignableToGeneric(typeof(Nullable<>)).Should().BeFalse();
                }

                [Fact]
                public void ItShouldIdentifyTheGenericGrandparentTypeDefinition() {
                    typeof(Bird).GetGenericParentType(typeof(Animal<>)).Should().Be(typeof(Animal<FlyingAnimal<Bird>>));
                }

                [Fact]
                public void ItShouldIdentifyTheGenericParentTypeDefinition() {
                    typeof(Bird).GetGenericParentType(typeof(FlyingAnimal<>)).Should().Be(typeof(FlyingAnimal<Bird>));
                }
            }
        }
    }
}
