using System;
using Cobweb.Testing.Mvc.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Fakes.SessionState {
    [TestFixture]
    public class GivenAPopulatedHttpSessionState {
        private const string ExpectedSessionKey = "SomeSessionKey";
        private DateTime _expectedItem;
        private FakeHttpSessionState _session;

        [SetUp]
        public void SetUp() {
            _expectedItem = DateTime.Now;
            var session = new FakeHttpSessionState();
            session[ExpectedSessionKey] = _expectedItem;
            session["anotherSessionItem"] = Guid.NewGuid();
            session["yetAnotherSessionItem"] = Guid.NewGuid();

            _session = session;
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfItems() {
            _session.Count.Should().Be(3);
        }

        [Test]
        public void ItShouldContainTheExpectedNumberOfKeys() {
            _session.Keys.Count.Should().Be(3);
        }

        [Test]
        public void ItShouldContainTheSpecifiedItem() {
            _session[ExpectedSessionKey].Should().Be(_expectedItem);
        }

        [Test]
        public void ItShouldContainZeroItemsOnAbandon() {
            _session.Abandon();
            _session.Count.Should().Be(0);
        }

        [Test]
        public void ItShouldContainZeroItemsOnClear() {
            _session.Abandon();
            _session.Count.Should().Be(0);
        }
    }
}
