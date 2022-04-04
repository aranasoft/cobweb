using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task ProcessQueueMessage(string message) {
            var taskRequest = JsonConvert.DeserializeObject<TaskRequest>(message);
            var taskRequestName = taskRequest.Name;
            var taskRequestTrackingId = taskRequest.TrackingId.ToString("D");
            _log.LogInformation("Received task {TaskRequestTrackingId} of type {TaskRequestName}", taskRequestTrackingId, taskRequestName);

            var handlers = _taskHandlerResolver.ResolveHandlers(taskRequest).ToList();

            if (!handlers.Any()) {
                _log.LogWarning("No task handlers found for task request {TaskRequestName}; exiting", taskRequestName);
                return;
            }

            if (! await ExecuteHandlersAsync(handlers))
                _log.LogWarning("One or more handler for {TaskRequestName} (Tracking: {TaskRequestTrackingId}) did not complete successfully", taskRequestName, taskRequestTrackingId);
        }

        private async Task<bool> ExecuteHandlersAsync(IEnumerable<ITaskHandler> handlers) {
            var handledSuccessfully = true;
            foreach (var taskHandler in handlers)
                try {
                    handledSuccessfully &= await taskHandler.HandleTaskAsync();
                }
                catch (Exception ex) {
                    var requestTrackingId = taskHandler.Request.TrackingId.ToString("D");
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
