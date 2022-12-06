using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using Aranasoft.Cobweb.Extensions;
using Azure.Core.Extensions;
using Azure.Messaging.ServiceBus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection {
    /// <summary>
    ///     Extension methods for setting up task queue related services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class TaskQueueServiceCollectionExtensions {
        /// <summary>
        ///   Registers a <see cref="TaskRequestQueue "/> instance with the provided <paramref name="connectionString"/>.
        /// </summary>
        public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueue<TBuilder>(this TBuilder builder, string connectionString)
        where TBuilder : IAzureClientFactoryBuilder
        {
            return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>(options => new TaskRequestQueue(new ServiceBusClient(connectionString, options), options.QueueName));
        }

        /// <summary>
        ///   Registers a <see cref="TaskRequestQueue"/> instance with the provided <paramref name="fullyQualifiedNamespace"/>.
        /// </summary>
        public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueueWithNamespace<TBuilder>(this TBuilder builder, string fullyQualifiedNamespace)
        where TBuilder : IAzureClientFactoryBuilderWithCredential
        {
            return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>((options, token) => new TaskRequestQueue(new ServiceBusClient(fullyQualifiedNamespace, token, options), options.QueueName));
        }

        /// <summary>
        ///   Registers a <see cref="TaskRequestQueue"/> instance with connection options loaded from the provided <paramref name="configuration"/> instance.
        /// </summary>
        public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueue<TBuilder, TConfiguration>(this TBuilder builder, TConfiguration configuration)
        where TBuilder : IAzureClientFactoryBuilderWithConfiguration<TConfiguration>
        {
            return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>(configuration);
        }

        /// <summary>
        ///     Registers the task queue processing services and handlers in the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="assemblies">The assemblies to scan for types deriving from <see cref="TaskHandler{TTaskRequest}" />.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddTaskQueueProcessing(
        this IServiceCollection serviceCollection,
        params Assembly[] assemblies) {
            serviceCollection = serviceCollection
            .AddTaskQueueProcessorServices();

            if (assemblies?.Any() == true)
                serviceCollection = serviceCollection.AddTaskHandlersFromAssemblies(assemblies);
            return serviceCollection;
        }

        private static IServiceCollection AddTaskQueueProcessorServices(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<ITaskHandlerResolver, TaskHandlerResolver>()
            .AddTransient<ITaskCoordinator, TaskCoordinator>();

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
