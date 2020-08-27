using System;
using System.Runtime.Serialization;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequestException : ApplicationException {
        public TaskRequestException() { }
        public TaskRequestException(string message) : base(message) {}
        public TaskRequestException(string message, Exception innerException) : base(message,innerException) {}
        public TaskRequestException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}
