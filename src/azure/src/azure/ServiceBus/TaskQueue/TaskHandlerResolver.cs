using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Resolves task handlers using a provided service provider.
    /// </summary>
    public class TaskHandlerResolver : ITaskHandlerResolver {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskHandlerResolver"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to use for resolving services.</param>
        public TaskHandlerResolver(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IEnumerable<ITaskHandler> ResolveHandlers(Type taskRequestType) {
            var handlerGenericType = typeof(TaskHandler<>);
            Type[] requestType = { taskRequestType };
            var handlerType = handlerGenericType.MakeGenericType(requestType);

            var services = _serviceProvider.GetServices(handlerType).Cast<ITaskHandler>();

            return services;
        }
    }
}
