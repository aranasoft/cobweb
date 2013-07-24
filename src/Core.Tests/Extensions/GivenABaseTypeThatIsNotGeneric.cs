using System;
using Aranasoft.Cobweb.Extentions;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenABaseTypeThatIsNotGeneric {
        public class Animal {}

        [Test]
        public void ItShouldNotIdentifyANonDerivedTypeAsDerived() {
            Action act = () => typeof (object).IsAssignableToGeneric(typeof (Animal));
            act.ShouldThrow<ArgumentException>();
        }
    }
}