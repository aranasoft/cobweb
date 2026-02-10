using System;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue;
public class GivenTaskCoordinator {
    private readonly TaskCoordinator _taskCoordinator;

    public GivenTaskCoordinator() {
        _taskCoordinator = new TaskCoordinator(null, new NullLogger<TaskCoordinator>());
    }

    [Fact]
    public void ItShouldDeserializeRequest() {
        var request = new TestTaskRequest(Guid.NewGuid(), 5);
        var message = request.ToJson();

        var deserializedRequest = _taskCoordinator.DeserializeTaskRequest(message);

        deserializedRequest.Should().BeOfType<TestTaskRequest>().Which.TrackingId.Should().Be(request.TrackingId);
        (deserializedRequest as TestTaskRequest)?.GetPayload().Should().Be(5);
    }

    public class TestTaskRequest : TaskRequest {
        private const string DataParameterName = "TestData";

        public TestTaskRequest(Guid trackingId, int payload) {
            TrackingId = trackingId;
            Parameters[DataParameterName] = JsonConvert.SerializeObject(payload);
        }

        public int GetPayload() {
            return JsonConvert.DeserializeObject<int>(Parameters[DataParameterName]);
        }
    }
}
