using System;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Attribute used to indicate the name to use for a task request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TaskRequestNameAttribute : Attribute {
        /// <summary>
        /// Gets the task request name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRequestNameAttribute"/> class with a given name.
        /// </summary>
        /// <param name="name">Name of the task request.</param>
        public TaskRequestNameAttribute(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Task Request name may not be null or whitespace");
            Name = name;
        }
    }
}
