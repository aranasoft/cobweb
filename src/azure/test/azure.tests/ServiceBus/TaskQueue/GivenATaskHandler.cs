using System;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenATaskHandler {
        private readonly TestTaskHandler _handler;

        public GivenATaskHandler() {
            _handler = new TestTaskHandler();
        }

        [Fact]
        public void ItShouldHandleRequests() {
            _handler.GetType().HandlesRequests().Should().BeTrue();
        }

        [Fact]
        public void ItShouldHandleTestRequest() {
            _handler.GetType().HandlesRequest(new TestTaskRequest()).Should().BeTrue();
        }

        [Fact]
        public void ItShouldNotHandleOtherRequest() {
            _handler.GetType().HandlesRequest(new UnhandledTestTaskRequest()).Should().BeFalse();
        }
        [TaskHandlesRequest(typeof(TestTaskRequest))]
        public class TestTaskHandler : TaskHandler {
            protected override Task<bool> ExecuteAsync(CancellationToken cancellationToken = default) {
                throw new NotImplementedException();
            }
        }

        [TaskRequestName("MyTaskRequest")]
        public class TestTaskRequest : TaskRequest {}

        [TaskRequestName("MyUnhandledTaskRequest")]
        public class UnhandledTestTaskRequest : TaskRequest {}
    }
}
