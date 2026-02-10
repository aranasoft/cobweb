using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Defines the methods for managing task requests in a queue.
/// </summary>
public interface ITaskRequestQueue {
    /// <summary>
    /// Adds a task request to the queue asynchronously.
    /// </summary>
    /// <param name="taskRequest">The <see cref="TaskRequest"/> to add to the queue.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task AddTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Schedules a task request to be added to the queue at a specified time asynchronously.
    /// </summary>
    /// <param name="request">The <see cref="TaskRequest"/> to schedule.</param>
    /// <param name="scheduledMessageEnqueueTime">The <see cref="DateTimeOffset"/> at which the task request should be added to the queue.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ScheduleTaskAsync(TaskRequest request, DateTimeOffset scheduledMessageEnqueueTime, CancellationToken cancellationToken = default);
}
