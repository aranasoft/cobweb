using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskProcessor : ITaskProcessor {
        private readonly ILogger<TaskProcessor> _log;
        private readonly ITaskHandlerResolver _taskHandlerResolver;

        public TaskProcessor(ITaskHandlerResolver taskHandlerResolver, ILogger<TaskProcessor> log) {
            _taskHandlerResolver = taskHandlerResolver;
            _log = log;
        }

        public async Task ProcessQueueMessageAsync(string message, CancellationToken cancellationToken = default) {
            var taskRequest = DeserializeTaskRequest(message);
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
                _log.LogWarning("One or more handler for {TaskRequestType} (Tracking: {TaskRequestTrackingId}) did not complete successfully", taskRequestType, taskRequestTrackingId);
        }

        public TaskRequest DeserializeTaskRequest(string message) {
            return JsonConvert.DeserializeObject<TaskRequest>(message, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
        }

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
