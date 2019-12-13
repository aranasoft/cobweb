using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskRequestQueue {
        Task AddTaskAsync(TaskRequest taskRequest, TimeSpan? delay = null);
        Task RegisterMessageHandlerAsync(Func<Message, CancellationToken, Task> callback, Func<ExceptionReceivedEventArgs, Task> exceptionCallback);
        Task RegisterMessageHandlerAsync(Func<Message, CancellationToken, Task> callback, MessageHandlerOptions options);
    }
}
