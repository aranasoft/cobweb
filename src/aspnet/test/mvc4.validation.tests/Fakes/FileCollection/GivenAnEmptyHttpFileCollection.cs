using System.Web;
using Aranasoft.Cobweb.Mvc.Validation.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Fakes.FileCollection {
    [TestFixture]
    public class GivenAnEmptyHttpFileCollection {
        private const string ExpectedFileKey = "SomeFileKey";
        private HttpFileCollectionBase _collection;

        [SetUp]
        public void SetUp() {
            var collection = new FakeHttpFileCollection();

            _collection = collection;
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfFiles() {
            _collection.Count.Should().Be(0);
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfKeys() {
            _collection.Keys.Count.Should().Be(0);
        }

        [Test]
        public void ItShouldNotContainTheSpecifiedFile() {
            _collection[ExpectedFileKey].Should().BeNull();
        }
    }
}
