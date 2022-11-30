using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskRequestQueue {
        Task AddTaskAsync(TaskRequest taskRequest, TimeSpan? delay = null, CancellationToken cancellationToken = default);
        Task RegisterErrorHandlerAsync(Func<ProcessErrorEventArgs, Task> exceptionCallback, CancellationToken cancellationToken = default);
        Task RegisterMessageHandlerAsync(Func<ProcessMessageEventArgs, Task> callback, CancellationToken cancellationToken = default);
    }
}
