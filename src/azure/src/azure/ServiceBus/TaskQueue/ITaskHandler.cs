using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskHandler {
        Type GetHandledRequestType();
        Task<bool> HandleTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default);
    }
}
