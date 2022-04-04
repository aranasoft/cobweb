using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskHandlerResolver : ITaskHandlerResolver {
        private readonly Dictionary<Type, Lazy<ITaskHandler>> _handlerContainers;
        private readonly ILogger<TaskHandlerResolver> _log;

        public TaskHandlerResolver(Dictionary<Type, Lazy<ITaskHandler>> handlerContainers, ILogger<TaskHandlerResolver> log) {
            _handlerContainers = handlerContainers;
            _log = log;
        }

        public IEnumerable<ITaskHandler> ResolveHandlers(TaskRequest taskRequest) {
            if (_log.IsEnabled(LogLevel.Debug)) {
                var taskRequestName = taskRequest.Name;
                var handlerCount = _handlerContainers.Count;
                _log.LogDebug("Resolving request {TaskRequestName} from {HandlerCount} handlers", taskRequestName, handlerCount);
            }

            var matchingHandlers = _handlerContainers.Where(container => container.Key.HandlesRequest(taskRequest))
            .Select(handlerContainer => {
                var handler = handlerContainer.Value.Value;
                handler.Request = taskRequest;
                return handler;
            });

            return matchingHandlers;
        }
    }
}
