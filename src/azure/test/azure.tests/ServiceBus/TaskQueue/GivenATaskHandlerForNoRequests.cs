using System;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenATaskHandlerForNoRequests {
        private readonly TestTaskHandler _handler;

        public GivenATaskHandlerForNoRequests() {
            _handler = new TestTaskHandler();
        }

        [Fact]
        public void ItShouldNotHandleRequests() {
            _handler.GetType().HandlesRequests().Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotHandleTestRequest() {
            _handler.GetType().HandlesRequest(new TestTaskRequest()).Should().BeFalse();
        }

        [Fact]
        public void ItShouldNotHandleOtherRequest() {
            _handler.GetType().HandlesRequest(new UnhandledTestTaskRequest()).Should().BeFalse();
        }

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
