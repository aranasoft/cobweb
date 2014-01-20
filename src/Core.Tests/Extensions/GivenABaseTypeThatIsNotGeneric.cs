using System;
using Cobweb.Extentions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
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