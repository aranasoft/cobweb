using System;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenTaskHandlerWithGenericDerivedBaseClass {
    private readonly TestTaskHandler _handler;

    public GivenTaskHandlerWithGenericDerivedBaseClass() {
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

    public class TestTaskHandler : BaseTestTaskHandler<TestTaskHandler> {
        protected override Task<bool> ExecuteAsync(
        TestTaskRequest taskRequest,
        CancellationToken cancellationToken = default) {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseTestTaskHandler<TTaskHandler> : TaskHandler<TestTaskRequest>
    where TTaskHandler : BaseTestTaskHandler<TTaskHandler> {}

    public class TestTaskRequest : TaskRequest {}

    public class UnhandledTestTaskRequest : TaskRequest {}
    }
}
