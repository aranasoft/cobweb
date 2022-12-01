using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskHandlerResolver : ITaskHandlerResolver {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TaskHandlerResolver> _log;

        public TaskHandlerResolver(IServiceProvider serviceProvider, ILogger<TaskHandlerResolver> log) {
            _serviceProvider = serviceProvider;
            _log = log;
        }

        public IEnumerable<ITaskHandler> ResolveHandlers(Type taskRequestType) {
            var handlerGenericType = typeof(TaskHandler<>);
            Type[] requestType = { taskRequestType };
            var handlerType = handlerGenericType.MakeGenericType(requestType);

            var services = _serviceProvider.GetServices(handlerType).Cast<ITaskHandler>();

            return services;
        }
    }
}
