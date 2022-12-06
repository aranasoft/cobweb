using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequestQueueOptions : ServiceBusClientOptions {
        public string QueueName { get; set; }
    }
}
