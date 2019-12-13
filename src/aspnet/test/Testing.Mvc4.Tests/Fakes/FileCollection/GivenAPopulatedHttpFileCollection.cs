using System.Web;
using Cobweb.Testing.Mvc.Fakes;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Fakes.FileCollection {
    [TestFixture]
    public class GivenAPopulatedHttpFileCollection {
        private const string ExpectedFileKey = "SomeFileKey";
        private HttpFileCollectionBase _collection;
        private HttpPostedFileBase _expectedFile;

        [SetUp]
        public void SetUp() {
            _expectedFile = Mock.Of<HttpPostedFileBase>();
            var collection = new FakeHttpFileCollection();
            collection[ExpectedFileKey] = _expectedFile;
            collection["anotherFile"] = Mock.Of<HttpPostedFileBase>();
            collection["yetAnotherFile"] = Mock.Of<HttpPostedFileBase>();

            _collection = collection;
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
    }
}
