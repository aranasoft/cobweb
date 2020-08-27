using System;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Attribute used to indicate the types of tasks requests handled by a task handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TaskHandlesRequestAttribute : Attribute {
        /// <summary>
        /// Gets the handled <see cref="TaskRequest"/> types.
        /// </summary>
        public Type[] RequestTypes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskHandlesRequestAttribute"/> class for given request types.
        /// </summary>
        /// <param name="requestTypes">Type of each <see cref="TaskRequest"/> handled.</param>
        public TaskHandlesRequestAttribute(params Type[] requestTypes) {
            RequestTypes = requestTypes ?? new Type[]{};
        }
    }
}
