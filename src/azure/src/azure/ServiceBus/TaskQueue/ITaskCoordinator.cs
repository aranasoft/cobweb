using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Defines the methods for coordinating tasks in a queue.
/// </summary>
public interface ITaskCoordinator {
    /// <summary>
    /// Processes a queue message asynchronously.
    /// </summary>
    /// <param name="message">The message to process.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ProcessQueueMessageAsync(string message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deserializes a task request from a message.
    /// </summary>
    /// <param name="message">The message that contains the serialized task request.</param>
    /// <returns>The deserialized <see cref="TaskRequest"/>.</returns>
    TaskRequest DeserializeTaskRequest(string message);
}
