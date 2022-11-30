using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public abstract class TaskRequestQueue : Queue, ITaskRequestQueue {
        protected TaskRequestQueue(ServiceBusAdministrationClient managementClient, ServiceBusClient queueClient, string taskQueueName) : base(managementClient, queueClient, taskQueueName) {}

        public async Task AddTaskAsync(TaskRequest request, TimeSpan? delay = null, CancellationToken cancellationToken = default) {
            var serializedRequest = JsonConvert.SerializeObject(request);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedRequest));
            if (delay != null) {
                message.Delay(delay.Value);
            }

            await SendMessageAsync(message, cancellationToken);
        }
    }
}
