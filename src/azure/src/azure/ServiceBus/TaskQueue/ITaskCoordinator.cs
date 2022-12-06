using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskCoordinator {
        Task ProcessQueueMessageAsync(string message, CancellationToken cancellationToken = default);
        TaskRequest DeserializeTaskRequest(string message);
    }
}
