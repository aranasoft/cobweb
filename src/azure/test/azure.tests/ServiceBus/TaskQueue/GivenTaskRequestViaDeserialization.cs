using System;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue;
public class GivenTaskRequestViaDeserialization {
    private readonly TaskRequest _deserializedRequest;

    public GivenTaskRequestViaDeserialization() {
        var request = new TestTaskRequest {TrackingId = Guid.Parse("d1e35239-6965-427d-8297-d630ff25bb94")};
        var serializedRequest = request.ToJson();
        _deserializedRequest = JsonConvert.DeserializeObject<TaskRequest>(serializedRequest, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
    }

    [Fact]
    public void ItShouldHaveTheMatchingTrackingId() {
        _deserializedRequest.TrackingId.Should().Be(Guid.Parse("d1e35239-6965-427d-8297-d630ff25bb94"));
    }

    [Fact]
    public void ItShouldBeTheCorrectType() {
        _deserializedRequest.Should().BeOfType<TestTaskRequest>();
    }

    public class TestTaskRequest : TaskRequest {}
}
