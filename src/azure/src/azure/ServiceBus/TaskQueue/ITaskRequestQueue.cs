using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskRequestQueue {
        Task AddTaskAsync(TaskRequest taskRequest, TimeSpan? delay = null);
        Task RegisterErrorHandlerAsync(Func<ProcessErrorEventArgs, Task> exceptionCallback);
        Task RegisterMessageHandlerAsync(Func<ProcessMessageEventArgs, Task> callback);
    }
}
