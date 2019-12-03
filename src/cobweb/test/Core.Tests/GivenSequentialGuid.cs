using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Cobweb.Tests {
    public class GivenSequentialGuid {
        [Fact]
        public void ItShouldGenerateUniqueGuids() {
            const int generatedValueCount = 1000;
            var values = new HashSet<Guid>();
            for (var i = 0; i < generatedValueCount; i++) {
                values.Add(SequentialGuid.NewGuid());
            }

            // All values should be unique
            values.Count.Should().Be(generatedValueCount);
        }
    }
}
