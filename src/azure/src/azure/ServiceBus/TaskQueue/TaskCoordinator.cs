using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Provides methods for coordinating tasks in a queue.
    /// </summary>
    public class TaskCoordinator : ITaskCoordinator {
        private readonly ILogger<TaskCoordinator> _log;
        private readonly ITaskHandlerResolver _taskHandlerResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRequestQueue"/> class.
        /// This constructor is intended for mocking purposes.
        /// </summary>
        protected TaskCoordinator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskCoordinator"/> class.
        /// </summary>
        /// <param name="taskHandlerResolver">The <see cref="ITaskHandlerResolver"/> to use for resolving task handlers.</param>
        /// <param name="log">The <see cref="ILogger"/> to use for logging.</param>
        public TaskCoordinator(ITaskHandlerResolver taskHandlerResolver, ILogger<TaskCoordinator> log) {
            _taskHandlerResolver = taskHandlerResolver;
            _log = log;
        }

        /// <inheritdoc />
        public virtual Task ProcessQueueMessageAsync(string message, CancellationToken cancellationToken = default) {
            var taskRequest = DeserializeTaskRequest(message);
            return ProcessTaskAsync(taskRequest, cancellationToken);
        }

        /// <summary>
        /// Processes a task request asynchronously.
        /// </summary>
        /// <param name="taskRequest">The <see cref="TaskRequest"/> to process.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public virtual async Task ProcessTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default) {
            var taskRequestType = taskRequest.GetType();
            var taskRequestTrackingId = taskRequest.TrackingId.ToString("D");
            _log.LogInformation("Received task {TaskRequestTrackingId} of type {TaskRequestType}",
                                taskRequestTrackingId,
                                taskRequestType);

            var handlers = _taskHandlerResolver.ResolveHandlers(taskRequestType).ToList();

            if (!handlers.Any()) {
                _log.LogWarning("No task handlers found for task request {TaskRequestType}; exiting", taskRequestType);
                return;
            }

            if (!await ExecuteHandlersAsync(handlers, taskRequest, cancellationToken))
                _log.LogWarning("One or more handler for {TaskRequestType} (Tracking: {TaskRequestTrackingId}) did not complete successfully",
                                taskRequestType,
                                taskRequestTrackingId);
        }

        /// <inheritdoc />
        public TaskRequest DeserializeTaskRequest(string message) {
            return JsonConvert.DeserializeObject<TaskRequest>(message, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
        }

        /// <summary>
        /// Executes the handlers for a task request asynchronously.
        /// </summary>
        /// <param name="handlers">The handlers to execute.</param>
        /// <param name="taskRequest">The <see cref="TaskRequest"/> to handle.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains a boolean indicating whether all handlers completed successfully.</returns>
        private async Task<bool> ExecuteHandlersAsync(IEnumerable<ITaskHandler> handlers, TaskRequest taskRequest, CancellationToken cancellationToken = default) {
            var handledSuccessfully = true;
            foreach (var taskHandler in handlers)
                try {
                    handledSuccessfully &= await taskHandler.HandleTaskAsync(taskRequest, cancellationToken);
                }
                catch (Exception ex) {
                    var requestTrackingId = taskRequest.TrackingId.ToString("D");
                    var handlerTypeName = taskHandler.GetType().FullName;
                    _log.LogError(
                        ex, "Unexpected Error with message: {RequestTrackingId} in handler {HandlerTypeName}",
                        requestTrackingId, handlerTypeName);
                    handledSuccessfully = false;
                }

            return handledSuccessfully;
        }
    }
}
