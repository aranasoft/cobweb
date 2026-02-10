using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Defines the methods for handling tasks in a queue.
/// </summary>
public interface ITaskHandler {
    /// <summary>
    /// Gets the type of the handled request.
    /// </summary>
    /// <returns>The <see cref="Type"/> of the handled request.</returns>
    Type GetHandledRequestType();

    /// <summary>
    /// Handles a task asynchronously.
    /// </summary>
    /// <param name="taskRequest">The <see cref="TaskRequest"/> to handle.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains a boolean indicating whether the task was handled successfully.</returns>
    Task<bool> HandleTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default);
}
