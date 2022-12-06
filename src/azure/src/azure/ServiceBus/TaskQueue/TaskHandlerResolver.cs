using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskHandlerResolver : ITaskHandlerResolver {
        private readonly IServiceProvider _serviceProvider;

        public TaskHandlerResolver(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
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
