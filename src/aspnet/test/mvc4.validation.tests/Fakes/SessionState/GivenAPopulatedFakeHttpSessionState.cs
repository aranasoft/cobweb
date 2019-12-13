using System;
using Cobweb.Testing.Mvc.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Fakes.SessionState {
    [TestFixture]
    public class GivenAPopulatedFakeHttpSessionState {
        private const string ExpectedSessionKey = "SomeSessionKey";
        private DateTime _expectedItem;
        private FakeHttpSessionState _session;

        [SetUp]
        public void SetUp() {
            _expectedItem = DateTime.Now;
            _session = new FakeHttpSessionState();
            _session[ExpectedSessionKey] = _expectedItem;
            _session["anotherSessionItem"] = Guid.NewGuid();
            _session["yetAnotherSessionItem"] = Guid.NewGuid();
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
        public void ItShouldAllowUpdatingAnItem() {
            var updatedItem = DateTime.Now.AddDays(-15);
            _session[ExpectedSessionKey] = updatedItem;
            _session[ExpectedSessionKey].Should().Be(updatedItem);
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
