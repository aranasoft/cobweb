using System;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenATaskRequest {
        private readonly TestTaskRequest _request;

        public GivenATaskRequest() {
            _request = new TestTaskRequest();
        }

        [Fact]
        public void ItShouldHaveAName() {
            _request.Name.Should().Be("MyTaskRequest");
        }

        [TaskRequestName("MyTaskRequest")]
        public class TestTaskRequest : TaskRequest {}
    }
}
