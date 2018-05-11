using Cobweb.Extentions;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenAnEnum {
        private enum Fruits {
            [System.ComponentModel.Description("Granny Smith Apples")]
            Apples,
            Oranges,
            Grapes
        }

        [Test]
        public void ItShouldReturnDescriptionElementsAsDescription() {
            Fruits.Apples.GetDescription().Should().Be("Granny Smith Apples");
        }

        [Test]
        public void ItShouldReturnNonDescriptionElementsAsEnumName() {
            Fruits.Oranges.GetDescription().Should().Be("Oranges");
        }
    }
}
