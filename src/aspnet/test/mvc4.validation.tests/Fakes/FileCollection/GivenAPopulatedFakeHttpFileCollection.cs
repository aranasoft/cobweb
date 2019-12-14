using System.Web;
using Aranasoft.Cobweb.Mvc.Validation.Fakes;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Mvc.Validation.Tests.Fakes.FileCollection {
    [TestFixture]
    public class GivenAPopulatedFakeHttpFileCollection {
        HttpPostedFileBase _expectedFile;
        const string ExpectedFileKey = "SomeFileKey";
        FakeHttpFileCollection _collection;

        [SetUp]
        public void SetUp() {
            _expectedFile = Mock.Of<HttpPostedFileBase>();
            _collection = new FakeHttpFileCollection();
            _collection[ExpectedFileKey] = _expectedFile;
            _collection["anotherFile"] = Mock.Of<HttpPostedFileBase>();
            _collection["yetAnotherFile"] = Mock.Of<HttpPostedFileBase>();
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfFiles() {
            _collection.Count.Should().Be(3);
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfKeys() {
            _collection.Keys.Count.Should().Be(3);
        }

        [Test]
        public void ItShouldContainTheSpecifiedFile() {
            _collection[ExpectedFileKey].Should().Be(_expectedFile);
        }


        [Test]
        public void ItShouldAllowUpdatingAFile() {
            var updatedFile = Mock.Of<HttpPostedFileBase>();
            _collection[ExpectedFileKey] = updatedFile;
            _collection[ExpectedFileKey].Should().Be(updatedFile);
        }
    }
}
