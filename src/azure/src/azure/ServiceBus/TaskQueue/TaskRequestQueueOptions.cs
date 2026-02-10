using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Represents the options for a task request queue.
/// </summary>
public class TaskRequestQueueOptions : ServiceBusClientOptions {
    /// <summary>
    /// Gets or sets the name of the queue.
    /// </summary>
    public string QueueName { get; set; }
}
