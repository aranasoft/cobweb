using System;
using Cobweb.Azure.Configuration;
using Cobweb.Azure.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Cobweb.TaskQueue {
    public interface ITaskRequestQueue {
        void AddTask(TaskRequest taskRequest, TimeSpan? delay = null);
        void OnMessage(Action<BrokeredMessage> message);
        void OnMessage(Action<BrokeredMessage> callback, OnMessageOptions options);
    }

    public class TaskRequestQueue : Queue, ITaskRequestQueue {
        private readonly IAzureConfiguration _azureConfiguration;

        public TaskRequestQueue(IAzureConfiguration azureConfiguration) {
            _azureConfiguration = azureConfiguration;
            ConnectionString = _azureConfiguration.ServiceBusConnectionString;
        }

        protected override string Name => _azureConfiguration.QueueName;
        protected sealed override string ConnectionString { get; set; }

        public void AddTask(TaskRequest request, TimeSpan? delay = null) {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var message = new BrokeredMessage(serializedRequest);
            if (delay != null) {
                var currentTime = DateTime.UtcNow;
                var executionTime = currentTime + delay;

                message.ScheduledEnqueueTimeUtc = executionTime.Value;
            }

            SendMessage(message);
        }
    }
}
