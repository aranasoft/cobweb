using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskHandlerResolver : ITaskHandlerResolver {
        private readonly Dictionary<Type, Lazy<ITaskHandler>> _handlerContainers;
        private readonly ILogger<TaskHandlerResolver> _log;

        public TaskHandlerResolver
        (Dictionary<Type, Lazy<ITaskHandler>> handlerContainers, ILogger<TaskHandlerResolver> log) {
            _handlerContainers = handlerContainers;
            _log = log;
        }

        public IEnumerable<ITaskHandler> ResolveHandlers(TaskRequest taskRequest) {
            if (_log.IsEnabled(LogLevel.Debug)) {
                _log.LogDebug($"Resolving request {taskRequest.Name} from {_handlerContainers.Count} handlers");
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
