using System;
using System.Runtime.Serialization;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
/// <summary>
/// Represents an exception that is thrown when a task request fails.
/// </summary>
public class TaskRequestException : ApplicationException {
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestException"/> class.
    /// </summary>
    public TaskRequestException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TaskRequestException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public TaskRequestException(string message, Exception innerException) : base(message,innerException) {}

#if !NET8_0_OR_GREATER
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskRequestException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    public TaskRequestException(SerializationInfo info, StreamingContext context) : base(info, context) {}
#endif
}
