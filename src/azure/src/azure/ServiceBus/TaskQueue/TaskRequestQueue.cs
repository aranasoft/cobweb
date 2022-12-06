using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequestQueue : ITaskRequestQueue {
        private readonly ServiceBusClient _queueClient;
        private ServiceBusSender _queueSender;

        private string QueueName { get; }

        public TaskRequestQueue(ServiceBusClient queueClient, string taskQueueName) {
            _queueClient = queueClient;
            QueueName = taskQueueName ?? throw new ArgumentNullException(nameof(taskQueueName), "Queue name for Task Queue is not specified");
        }

        public Task AddTaskAsync(TaskRequest request, CancellationToken cancellationToken = default) {
            var serializedRequest = request.ToJson();
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedRequest));
            return GetQueueSender().SendMessageAsync(message, cancellationToken);
        }

        public Task ScheduleTaskAsync(TaskRequest request, DateTimeOffset scheduledMessageEnqueueTime, CancellationToken cancellationToken = default) {
            var serializedRequest = request.ToJson();
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedRequest));
            return GetQueueSender().ScheduleMessageAsync(message, scheduledMessageEnqueueTime, cancellationToken);
        }

        private ServiceBusSender GetQueueSender() {
            return _queueSender ??= _queueClient.CreateSender(QueueName);
        }
    }
}
