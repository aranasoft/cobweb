using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Manages task requests in a queue using Azure Service Bus.
/// </summary>
public class TaskRequestQueue : ITaskRequestQueue {
    private readonly ServiceBusClient _queueClient;
    private ServiceBusSender _queueSender;

    /// <summary>
    /// Gets the name of the queue.
    /// </summary>
    private string QueueName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestQueue"/> class.
    /// This constructor is intended for mocking purposes.
    /// </summary>
    protected TaskRequestQueue()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestQueue"/> class.
    /// </summary>
    /// <param name="queueClient">The <see cref="ServiceBusClient"/> to use for interacting with the queue.</param>
    /// <param name="taskQueueName">The name of the task queue.</param>
    /// <exception cref="ArgumentNullException">Thrown when the task queue name is not specified.</exception>
    public TaskRequestQueue(ServiceBusClient queueClient, string taskQueueName) {
        _queueClient = queueClient;
        QueueName = taskQueueName ?? throw new ArgumentNullException(nameof(taskQueueName), "Queue name for Task Queue is not specified");
    }

    /// <inheritdoc />
    public virtual Task AddTaskAsync(TaskRequest request, CancellationToken cancellationToken = default) {
        var serializedRequest = request.ToJson();
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedRequest));
        return GetQueueSender().SendMessageAsync(message, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task ScheduleTaskAsync(TaskRequest request, DateTimeOffset scheduledMessageEnqueueTime, CancellationToken cancellationToken = default) {
        var serializedRequest = request.ToJson();
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedRequest));
        return GetQueueSender().ScheduleMessageAsync(message, scheduledMessageEnqueueTime, cancellationToken);
    }

    /// <summary>
    /// Gets the <see cref="ServiceBusSender"/> for the queue.
    /// </summary>
    /// <returns>The <see cref="ServiceBusSender"/> for the queue.</returns>
    private ServiceBusSender GetQueueSender() {
        return _queueSender ??= _queueClient.CreateSender(QueueName);
    }
}
