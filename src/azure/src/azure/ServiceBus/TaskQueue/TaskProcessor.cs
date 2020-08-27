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
            _log.LogDebug($"Received task {taskRequest.TrackingId:D} of type {taskRequest.Name}");

            var handlers = _taskHandlerResolver.ResolveHandlers(taskRequest).ToList();

            if (!handlers.Any()) {
                _log.LogWarning("No task handlers found for task request " + taskRequest.Name + ". Exiting.");
                return;
            }

            if (! await ExecuteHandlersAsync(handlers))
                _log.LogWarning($"One or more handler for {taskRequest.Name} (Tracking: {taskRequest.TrackingId:D}) did not complete successfully.");
        }

        private async Task<bool> ExecuteHandlersAsync(IEnumerable<ITaskHandler> handlers) {
            var handledSuccessfully = true;
            foreach (var taskHandler in handlers)
                try {
                    handledSuccessfully &= await taskHandler.HandleTaskAsync();
                }
                catch (Exception ex) {
                    _log.LogError(
                        $"Unexpected Error with message: {taskHandler.Request.TrackingId:D} in handler {taskHandler.GetType().FullName}",
                        ex);
                }

            return handledSuccessfully;
        }
    }
}
