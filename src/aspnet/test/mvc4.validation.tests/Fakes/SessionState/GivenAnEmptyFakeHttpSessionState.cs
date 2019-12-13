using System;
using Cobweb.Testing.Mvc.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Fakes.SessionState {
    [TestFixture]
    public class GivenAnEmptyFakeHttpSessionState {
        private const string ExpectedSessionKey = "SomeSessionKey";
        private FakeHttpSessionState _session;

        [SetUp]
        public void SetUp() {
            _session = new FakeHttpSessionState();
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

        [Test]
        public void ItShouldAllowAddingAItem() {
            var newSessionItem = new DateTime();
            _session[ExpectedSessionKey] = newSessionItem;
            _session[ExpectedSessionKey].Should().Be(newSessionItem);
        }
    }
}
