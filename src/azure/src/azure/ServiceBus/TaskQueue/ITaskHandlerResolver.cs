using System;
using System.Collections.Generic;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Defines the methods for resolving task handlers.
/// </summary>
public interface ITaskHandlerResolver {
    /// <summary>
    /// Resolves the handlers for a specific task request type.
    /// </summary>
    /// <param name="taskRequestType">The <see cref="Type"/> of the task request for which to resolve handlers.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ITaskHandler"/> that can handle the specified task request type.</returns>
    IEnumerable<ITaskHandler> ResolveHandlers(Type taskRequestType);
}
