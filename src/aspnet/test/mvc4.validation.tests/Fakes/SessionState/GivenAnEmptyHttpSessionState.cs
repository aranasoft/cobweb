using System.Web;
using Cobweb.Testing.Mvc.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Fakes.SessionState {
    [TestFixture]
    public class GivenAnEmptyHttpSessionState {
        private const string ExpectedSessionKey = "SomeSessionKey";
        private HttpSessionStateBase _session;

        [SetUp]
        public void SetUp() {
            var collection = new FakeHttpSessionState();

            _session = collection;
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfItems() {
            _session.Count.Should().Be(0);
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfKeys() {
            _session.Keys.Count.Should().Be(0);
        }

        [Test]
        public void ItShouldNotContainTheSpecifiedItem() {
            _session[ExpectedSessionKey].Should().BeNull();
        }
    }
}
