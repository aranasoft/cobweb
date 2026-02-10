using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using Aranasoft.Cobweb.Extensions;
using Azure.Core.Extensions;
using Azure.Messaging.ServiceBus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
/// <summary>
///     Extension methods for setting up task queue related services in an <see cref="IServiceCollection" />.
/// </summary>
public static class TaskQueueServiceCollectionExtensions {
    /// <summary>
    ///   Registers a <see cref="TaskRequestQueue "/> instance with the provided connection string.
    /// </summary>
    /// <param name="builder">The builder to register the client factory with.</param>
    /// <param name="connectionString">The connection string to the Azure Service Bus.</param>
    /// <returns>An Azure client builder for chaining further configuration.</returns>
    public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueue<TBuilder>(this TBuilder builder, string connectionString)
    where TBuilder : IAzureClientFactoryBuilder
    {
        return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>(options => new TaskRequestQueue(new ServiceBusClient(connectionString, options), options.QueueName));
    }

    /// <summary>
    ///   Registers a <see cref="TaskRequestQueue"/> instance with the provided fully qualified namespace.
    /// </summary>
    /// <param name="builder">The builder to register the client factory with.</param>
    /// <param name="fullyQualifiedNamespace">The fully qualified namespace of the Azure Service Bus.</param>
    /// <returns>An Azure client builder for chaining further configuration.</returns>
    public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueueWithNamespace<TBuilder>(this TBuilder builder, string fullyQualifiedNamespace)
    where TBuilder : IAzureClientFactoryBuilderWithCredential
    {
        return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>((options, token) => new TaskRequestQueue(new ServiceBusClient(fullyQualifiedNamespace, token, options), options.QueueName));
    }

    /// <summary>
    ///   Registers a <see cref="TaskRequestQueue"/> instance with connection options loaded from the provided configuration instance.
    /// </summary>
    /// <param name="builder">The builder to register the client factory with.</param>
    /// <param name="configuration">The configuration instance containing connection options.</param>
    /// <returns>An Azure client builder for chaining further configuration.</returns>
    public static IAzureClientBuilder<TaskRequestQueue, TaskRequestQueueOptions> AddTaskQueue<TBuilder, TConfiguration>(this TBuilder builder, TConfiguration configuration)
    where TBuilder : IAzureClientFactoryBuilderWithConfiguration<TConfiguration>
    {
        return builder.RegisterClientFactory<TaskRequestQueue, TaskRequestQueueOptions>(configuration);
    }

    /// <summary>
    ///   Registers the task queue processing services and handlers in the <see cref="IServiceCollection" />.
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

    /// <summary>
    ///   Registers the task queue processor services in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    private static IServiceCollection AddTaskQueueProcessorServices(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransient<ITaskHandlerResolver, TaskHandlerResolver>()
        .AddTransient<ITaskCoordinator, TaskCoordinator>();

    /// <summary>
    ///   Registers task handlers from the provided assemblies in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="assemblies">The assemblies to scan for task handlers.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    private static IServiceCollection AddTaskHandlersFromAssemblies(
    this IServiceCollection serviceCollection,
    params Assembly[] assemblies) {
        return assemblies.Aggregate(serviceCollection, AddTaskHandlersFromAssembly);
    }

    /// <summary>
    ///   Registers task handlers from the provided assembly in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="assembly">The assembly to scan for task handlers.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    private static IServiceCollection AddTaskHandlersFromAssembly(
    this IServiceCollection serviceCollection,
    Assembly assembly) {
        var handlerTypes = assembly.GetTypes().Where(type => type.IsTaskHandler()).ToList();
        return serviceCollection.AddTaskHandlers(handlerTypes);
    }

    /// <summary>
    ///   Registers task handlers from the provided types in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="taskHandlerTypes">The types of the task handlers to register.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddTaskHandlers(
    this IServiceCollection serviceCollection,
    IEnumerable<Type> taskHandlerTypes) => taskHandlerTypes.Aggregate(serviceCollection, AddTaskHandler);

    /// <summary>
    ///   Registers a task handler of the provided type in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddTaskHandler<TTaskHandler, TTaskRequest>(this IServiceCollection serviceCollection) where TTaskHandler : TaskHandler<TTaskRequest> where TTaskRequest : TaskRequest
        => serviceCollection.AddTaskHandler(typeof(TTaskHandler));

    /// <summary>
    ///   Registers a task handler of the provided type in the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="taskHandlerType">The type of the task handler to register.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddTaskHandler(
    this IServiceCollection serviceCollection,
    Type taskHandlerType) {
        var baseTypeDefinition = taskHandlerType.GetGenericParentType(typeof(TaskHandler<>));
        return serviceCollection.AddTransient(baseTypeDefinition, taskHandlerType)
        .AddTransient(typeof(ITaskHandler), taskHandlerType);
    }
}
