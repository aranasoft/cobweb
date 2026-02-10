using System;
using System.Linq;
using Aranasoft.Cobweb.Extensions;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Provides extension methods for task handlers.
/// </summary>
public static class TaskHandlerExtensions {
    /// <summary>
    /// Determines whether a type is a task handler.
    /// </summary>
    /// <param name="handlerType">The <see cref="Type"/> to check.</param>
    /// <returns><c>true</c> if the type is a task handler; otherwise, <c>false</c>.</returns>
    public static bool IsTaskHandler(this Type handlerType) {
        return handlerType.IsClass &&
               !handlerType.IsAbstract &&
               handlerType.BaseType?.IsAssignableToGeneric(typeof(TaskHandler<>)) == true;
    }

    /// <summary>
    /// Gets the type of the task request that a task handler handles.
    /// </summary>
    /// <param name="handlerType">The <see cref="Type"/> of the task handler.</param>
    /// <returns>The <see cref="Type"/> of the task request that the task handler handles.</returns>
    /// <exception cref="ArgumentException">Thrown when the type is not assignable to <see cref="TaskHandler{TTaskRequest}"/>.</exception>
    public static Type GetHandledRequestType(this Type handlerType) {
        if (!handlerType.IsAssignableToGeneric(typeof(TaskHandler<>))) {
            throw new ArgumentException($"Type is not assignable to {typeof(TaskHandler<>).Name}", nameof(handlerType));
        }

        var handlerBaseType = handlerType.GetGenericParentType(typeof(TaskHandler<>));
        var requestType = handlerBaseType.GenericTypeArguments.First();

        return requestType;
    }

    /// <summary>
    /// Determines whether a task handler handles a specific type of task request.
    /// </summary>
    /// <param name="handlerType">The <see cref="Type"/> of the task handler.</param>
    /// <param name="taskRequestType">The <see cref="Type"/> of the task request.</param>
    /// <returns><c>true</c> if the task handler handles the specified type of task request; otherwise, <c>false</c>.</returns>
    public static bool HandlesRequest(this Type handlerType, Type taskRequestType) {
        var handledRequestType = handlerType.GetHandledRequestType();

        return taskRequestType.IsAssignableTo(handledRequestType);
    }
}
