using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenATaskRequestWithAnOverridenName {
        private readonly TestTaskRequest _request;

        public GivenATaskRequestWithAnOverridenName() {
            _request = new TestTaskRequest {Name = "SpecifiedTaskName"};
        }

        [Fact]
        public void ItShouldHaveAName() {
            _request.Name.Should().Be("SpecifiedTaskName");
        }

        [TaskRequestName("MyOverridenTaskRequest")]
        public class TestTaskRequest : TaskRequest {}
    }
}
