using System;
using System.Text;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.Configuration;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequestQueue : Queue, ITaskRequestQueue {
        public TaskRequestQueue(string connectionString, string name) : base(connectionString, name) {}
        public TaskRequestQueue(IAzureConfiguration azureConfiguration) : base(azureConfiguration) {}

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
