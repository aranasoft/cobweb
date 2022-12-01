using System;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenTaskHandler {
        private readonly TestTaskHandler _handler;

        public GivenTaskHandler() {
            _handler = new TestTaskHandler();
        }

        [Fact]
        public void ItShouldHandleRequests() {
            _handler.GetType().IsTaskHandler().Should().BeTrue();
        }

        [Fact]
        public void ItShouldHandleTestRequest() {
            _handler.GetType().HandlesRequest(typeof(TestTaskRequest)).Should().BeTrue();
        }

        [Fact]
        public void ItShouldNotHandleOtherRequest() {
            _handler.GetType().HandlesRequest(typeof(UnhandledTestTaskRequest)).Should().BeFalse();
        }

        public class TestTaskHandler : TaskHandler<TestTaskRequest> {
            protected override Task<bool> ExecuteAsync(TestTaskRequest taskRequest, CancellationToken cancellationToken = default) {
                throw new NotImplementedException();
            }
        }

        public class TestTaskRequest : TaskRequest {}

        public class UnhandledTestTaskRequest : TaskRequest {}
    }
}
