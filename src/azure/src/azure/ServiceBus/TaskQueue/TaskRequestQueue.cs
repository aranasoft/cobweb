using System;
using System.Text;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.Configuration;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public abstract class TaskRequestQueue : Queue, ITaskRequestQueue {
        protected TaskRequestQueue(string connectionString, string taskQueueName) : base(connectionString, taskQueueName) {}
        protected TaskRequestQueue(IAzureConfiguration azureConfiguration, string taskQueueName) : base(azureConfiguration, taskQueueName) {}

        public async Task AddTaskAsync(TaskRequest request, TimeSpan? delay = null) {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var message = new Message(Encoding.UTF8.GetBytes(serializedRequest));
            if (delay != null) {
                message.Delay(delay.Value);
            }

            await SendMessageAsync(message);
        }
    }
}
