using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenADeserializedTaskRequest {
        private readonly TaskRequest _deserializedRequest;

        public GivenADeserializedTaskRequest() {
            var request = new TestTaskRequest {Name = "SpecifiedTaskName"};
            var serializedRequest = JsonConvert.SerializeObject(request);
            _deserializedRequest = JsonConvert.DeserializeObject<TaskRequest>(serializedRequest);
        }

        [Fact]
        public void ItShouldHaveAName() {
            _deserializedRequest.Name.Should().Be("SpecifiedTaskName");
        }

        [TaskRequestName("MyDeserializedTaskRequest")]
        public class TestTaskRequest : TaskRequest {}
    }
}
