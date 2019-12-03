using System;
using Cobweb.Extentions;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests.Extensions {
    public class GivenABaseTypeThatIsNotGeneric {
        [Fact]
        public void ItShouldThrowWhenCheckingGenericAssignment() {
            Action act = () => typeof(object).IsAssignableToGeneric(typeof(Animal));
            act.Should().Throw<ArgumentException>();
        }

        public class Animal {}

    
        public class GivenANonGenericDerivedType {
            [Fact]
            public void ItShouldIdentifyBaseClassAsAssignableTo() {
                typeof(Fish).IsAssignableTo(typeof(Animal)).Should().BeTrue();
            }

            [Fact]
            public void ItShouldIdentifyBaseClassAsAssignableToViaConstraints() {
                typeof(Fish).IsAssignableTo<Animal>().Should().BeTrue();
            }

            [Fact]
            public void ItShouldNotIdentifyDerivedClassAsAssignableTo() {
                typeof(Animal).IsAssignableTo(typeof(Fish)).Should().BeFalse();
            }

            [Fact]
            public void ItShouldNotIdentifyDerivedClassAsAssignableToViaConstraints() {
                typeof(Animal).IsAssignableTo<Fish>().Should().BeFalse();
            }

            [Fact]
            public void ItShouldIdentifyDerivedClassAsAssignableFrom() {
                typeof(Animal).IsAssignableFrom(typeof(Fish)).Should().BeTrue();
            }

            [Fact]
            public void ItShouldIdentifyDerivedClassAsAssignableFromViaConstraints() {
                typeof(Animal).IsAssignableFrom<Fish>().Should().BeTrue();
            }

            [Fact]
            public void ItShouldNotIdentifyBaseClassAsAssignableFrom() {
                typeof(Fish).IsAssignableFrom(typeof(Animal)).Should().BeFalse();
            }

            [Fact]
            public void ItShouldNotIdentifyBaseClassAsAssignableFromViaConstraints() {
                typeof(Fish).IsAssignableFrom<Animal>().Should().BeFalse();
            }

            public class Fish : Animal {}
        }
    }
}
