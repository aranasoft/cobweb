using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using Aranasoft.Cobweb.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection {
    /// <summary>
    ///     Extension methods for setting up task queue related services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class TaskQueueServiceCollectionExtensions {
        /// <summary>
        ///     Registers the task queue services and handlers in the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="assemblies">The assemblies to scan for types deriving from <see cref="TaskHandler{TTaskRequest}" />.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddTaskQueue(
        this IServiceCollection serviceCollection,
        params Assembly[] assemblies) {
            serviceCollection = serviceCollection.AddTaskQueueExecutionServices();
            if (assemblies?.Any() == true)
                serviceCollection = serviceCollection.AddTaskHandlersFromAssemblies(assemblies);
            return serviceCollection;
        }

        private static IServiceCollection AddTaskQueueExecutionServices(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<ITaskHandlerResolver, TaskHandlerResolver>()
            .AddTransient<ITaskProcessor, TaskProcessor>();

        private static IServiceCollection AddTaskHandlersFromAssemblies(
        this IServiceCollection serviceCollection,
        params Assembly[] assemblies) {
            return assemblies.Aggregate(serviceCollection, AddTaskHandlersFromAssembly);
        }

        private static IServiceCollection AddTaskHandlersFromAssembly(
        this IServiceCollection serviceCollection,
        Assembly assembly) {
            var handlerTypes = assembly.GetTypes().Where(type => type.IsTaskHandler()).ToList();
            return serviceCollection.AddTaskHandlers(handlerTypes);
        }

        public static IServiceCollection AddTaskHandlers(
        this IServiceCollection serviceCollection,
        IEnumerable<Type> taskHandlerTypes) => taskHandlerTypes.Aggregate(serviceCollection, AddTaskHandler);

        public static IServiceCollection AddTaskHandler<TTaskHandler, TTaskRequest>(this IServiceCollection serviceCollection) where TTaskHandler : TaskHandler<TTaskRequest> where TTaskRequest : TaskRequest
            => serviceCollection.AddTaskHandler(typeof(TTaskHandler));

        public static IServiceCollection AddTaskHandler(
        this IServiceCollection serviceCollection,
        Type taskHandlerType) {
            var baseTypeDefinition = taskHandlerType.GetGenericParentType(typeof(TaskHandler<>));
            return serviceCollection.AddTransient(baseTypeDefinition, taskHandlerType)
            .AddTransient(typeof(ITaskHandler), taskHandlerType);
        }
    }
}
