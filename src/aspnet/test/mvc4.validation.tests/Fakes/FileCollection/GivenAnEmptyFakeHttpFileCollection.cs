using System.Web;
using Aranasoft.Cobweb.Mvc.Validation.Fakes;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Fakes.FileCollection {
    [TestFixture]
    public class GivenAnEmptyFakeHttpFileCollection {
        const string ExpectedFileKey = "SomeFileKey";
        FakeHttpFileCollection _collection;

        [SetUp]
        public void SetUp() {
            _collection = new FakeHttpFileCollection();
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


        [Test]
        public void ItShouldAllowAddingAFile() {
            var updatedFile = Mock.Of<HttpPostedFileBase>();
            _collection[ExpectedFileKey] = updatedFile;
            _collection[ExpectedFileKey].Should().Be(updatedFile);
        }
    }
}
