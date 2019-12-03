using Cobweb.Extentions;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cobweb.Tests.Extensions {
    public class GivenAnEnumerable {
        private IEnumerable<int> _collection;

        public GivenAnEnumerable() {
            _collection = Enumerable.Range(1, 5);
        }

        [Fact]
        public void ItShouldExecuteDelegateForEachItem() {
            var executions = 0;
            var expectedExecutions = _collection.Count();

            _collection.ForEach(str => executions++);

            executions.Should().Be(expectedExecutions);
        }
    }
}
