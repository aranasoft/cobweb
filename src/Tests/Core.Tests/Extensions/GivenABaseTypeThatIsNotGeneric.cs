using System;
using Cobweb.Extentions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenABaseTypeThatIsNotGeneric {
        [Test]
        public void ItShouldThrowWhenCheckingGenericAssignment() {
            Action act = () => typeof(object).IsAssignableToGeneric(typeof(Animal));
            act.ShouldThrow<ArgumentException>();
        }

        public class Animal {}

        [TestFixture]
        public class GivenANonGenericDerivedType {
            [Test]
            public void ItShouldIdentifyBaseClassAsAssignableTo() {
                typeof(Fish).IsAssignableTo(typeof(Animal)).Should().BeTrue();
            }

            [Test]
            public void ItShouldIdentifyBaseClassAsAssignableToViaConstraints() {
                typeof(Fish).IsAssignableTo<Animal>().Should().BeTrue();
            }

            [Test]
            public void ItShouldNotIdentifyDerivedClassAsAssignableTo() {
                typeof(Animal).IsAssignableTo(typeof(Fish)).Should().BeFalse();
            }

            [Test]
            public void ItShouldNotIdentifyDerivedClassAsAssignableToViaConstraints() {
                typeof(Animal).IsAssignableTo<Fish>().Should().BeFalse();
            }

            [Test]
            public void ItShouldIdentifyDerivedClassAsAssignableFrom() {
                typeof(Animal).IsAssignableFrom(typeof(Fish)).Should().BeTrue();
            }

            [Test]
            public void ItShouldIdentifyDerivedClassAsAssignableFromViaConstraints() {
                typeof(Animal).IsAssignableFrom<Fish>().Should().BeTrue();
            }

            [Test]
            public void ItShouldNotIdentifyBaseClassAsAssignableFrom() {
                typeof(Fish).IsAssignableFrom(typeof(Animal)).Should().BeFalse();
            }

            [Test]
            public void ItShouldNotIdentifyBaseClassAsAssignableFromViaConstraints() {
                typeof(Fish).IsAssignableFrom<Animal>().Should().BeFalse();
            }

            public class Fish : Animal {}
        }
    }
}
