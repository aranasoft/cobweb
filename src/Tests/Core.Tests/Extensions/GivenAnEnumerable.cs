using Cobweb.Extentions;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cobweb.Tests.Extensions {
    [TestFixture]
    public class GivenAnEnumerable {
        private IEnumerable<int> _collection;

        [SetUp]
        public void SetUp() {
            _collection = Enumerable.Range(1, 5);
        }

        [Test]
        public void ItShouldExecuteDelegateForEachItem() {
            var executions = 0;
            var expectedExecutions = _collection.Count();

            _collection.ForEach(str => executions++);

            executions.Should().Be(expectedExecutions);
        }
    }
}
